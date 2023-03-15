using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks.Dataflow;
using The_fifth_group_FinalProject.Models;

namespace The_fifth_group_FinalProject.Controllers
{
    public class WebSocketController : Controller
    {
        private readonly TheFifthGroupOfTopicsContext _theFifthGroupOfTopicsContext;

        public WebSocketController(TheFifthGroupOfTopicsContext theFifthGroupOfTopicsContext)
        {
            _theFifthGroupOfTopicsContext = theFifthGroupOfTopicsContext;
        }
        static ConcurrentDictionary<int, WebSocket> WebSockets = new ConcurrentDictionary<int, WebSocket>();

        public IActionResult Index()
        {
            string account = HttpContext.Session.GetString("Account");
            var httpContext = HttpContext;
            int MemberLoginId = int.TryParse(httpContext.Request.Cookies["MemberId"], out int result) ? result : 0;
            var MemberName = _theFifthGroupOfTopicsContext.Members.Where(m => m.MemberId == MemberLoginId).Select(m => m.Name);

            if (!string.IsNullOrEmpty(account))
            {
                // 已登入
                ViewBag.Account = account;
            }
            else if (MemberName.Any())
            {
                // 員工已登入
                ViewBag.Account = MemberName.First();
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
            return View();
        }

        [Route("{Controller}/ws")]
        public async Task Get()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await ProcessWebSocket(webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

        public async Task ProcessWebSocket(WebSocket webSocket)
        {
           
            WebSockets.TryAdd(webSocket.GetHashCode(), webSocket);
            var buffer = new byte[1024 * 4];
            var res = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            string? UserName = null;
            while (!res.CloseStatus.HasValue)
            {
                UserName = "匿名";
                var cmd = Encoding.UTF8.GetString(buffer, 0, res.Count);
                JObject data = JObject.Parse(cmd);
                string? Name = Convert.ToString(data["userName"]);
                string? Message = $"{Convert.ToString(data["message"])} at {DateTime.Now}";
                var httpContext = HttpContext;
                int MemberLoginId = int.TryParse(httpContext.Request.Cookies["MemberId"], out int result) ? result : 1;
                string account = HttpContext.Session.GetString("Account");
                var EmployeeId = _theFifthGroupOfTopicsContext.Employees.Where(m => m.Account == account).Select(m => m.Id);
                int EmployeeLoginId = EmployeeId.First();
                if (!string.IsNullOrEmpty(Name))
                {
                    UserName = Name;
                }
                // 自動回覆功能
                
                Broadcast(JsonConvert.SerializeObject(new
                {
                    userName = UserName,
                    message = Message
                }));
                await _theFifthGroupOfTopicsContext.ChatContents.AddAsync(new ChatContent
                {
                    MemberId = MemberLoginId,
                    ChatContent1 = Message,
                    SentTime = DateTime.Now,
                    ChatRoomId = 1,
                    EmployeeId = EmployeeLoginId,
                });
                await _theFifthGroupOfTopicsContext.SaveChangesAsync();

                var autoReplyContent = await FindAutoReplyContentAsync(Message);

                if (!string.IsNullOrEmpty(autoReplyContent))
                {
                    Message = autoReplyContent;
                    Broadcast(JsonConvert.SerializeObject(new
                    {
                        userName = "客服",
                        message = $"{Message}at {DateTime.Now}"
                    }));
                }
                

                res = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await webSocket.CloseAsync(res.CloseStatus.Value, res.CloseStatusDescription, CancellationToken.None);
            WebSockets.TryRemove(webSocket.GetHashCode(), out var removed); // removed允許未宣告
            Broadcast(JsonConvert.SerializeObject(new
            {
                userName = UserName,
                message = "離開聊天室"
            }));
        }

        public void Broadcast(string message)
        {
            var buff = Encoding.UTF8.GetBytes($"{message}");
            var data = new ArraySegment<byte>(buff, 0, buff.Length);
            Parallel.ForEach(WebSockets.Values, async (webSocket) =>
            {
                if (webSocket.State == WebSocketState.Open)
                    await webSocket.SendAsync(data, WebSocketMessageType.Text, true, CancellationToken.None);
            });
        }
        // 找尋符合的自動回覆內容
        private async Task<string> FindAutoReplyContentAsync(string message)
        {
            // 建立一個字典來紀錄每個回覆內容符合的關鍵字數量
            var autoReplyCounts = new Dictionary<AutoReply, int>();

            // 取得所有的關鍵字回覆
            var autoReplies = await _theFifthGroupOfTopicsContext.AutoReplies.Include(a => a.AutoReplyKeyWords)
                .ToListAsync();

            // 對於每個關鍵字回覆，計算符合的關鍵字數量
            foreach (var autoReply in autoReplies)
            {
                int count = 0;
                foreach (var keyword in autoReply.AutoReplyKeyWords)
                {
                    if (message.Contains(keyword.KeyWord))
                    {
                        count++;
                    }
                }
                autoReplyCounts[autoReply] = count;
            }

            // 如果沒有符合任何關鍵字回覆，回傳空字串
            if (autoReplyCounts.Values.Max() == 0)
            {
                return "";
            }

            // 找到符合最多關鍵字的回覆內容
            var maxCount = autoReplyCounts.Values.Max();
            var autoReplyContent = autoReplyCounts.FirstOrDefault(a => a.Value == maxCount).Key.AutoReplyContent;

            return autoReplyContent;
        }


    }
}

