using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Packaging.Signing;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks.Dataflow;

namespace The_fifth_group_FinalProject.Controllers
{
    public class WebSocketController : Controller
    {
        static ConcurrentDictionary<int, WebSocket> WebSockets = new ConcurrentDictionary<int, WebSocket>();

        public IActionResult Index()
        {
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
                string timestamp = Convert.ToString(data["sendTime"]);
                DateTime messageTime = DateTime.Parse(timestamp);
                if (!string.IsNullOrEmpty(Name))
                {
                    UserName = Name;
                }
                Broadcast(JsonConvert.SerializeObject(new
                {
                    userName = UserName,
                    message = Message
                }));
                using (var conn = new SqlConnection("Server=.\\mssqllocaldb;Database=TheFifthGroupOfTopics"))
                {
                    await conn.OpenAsync();
                    var cmdsql = new SqlCommand("INSERT INTO ChatContents (MemberId, ChatContent, SentTime, ChatRoomId, EmployeeId) SELECT j.MemberId, j.ChatContent, j.SentTime, j.ChatRoomId, j.EmployeeId FROM OPENJSON(@json) WITH (MemberId INT, ChatContent NVARCHAR(MAX), SentTime DATETIME, ChatRoomId INT, EmployeeId INT) as j", conn);
                    var json = JsonConvert.SerializeObject(new
                    {
                        MemberId = 1,
                        ChatContent = Message,
                        SentTime = DateTime.Now,
                        ChatRoomId = 1,
                        EmployeeId = 1
                    });
                    cmdsql.Parameters.AddWithValue("@json", json);
                    await cmdsql.ExecuteNonQueryAsync();
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
    }
}

