using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace The_fifth_group_FinalProject.Controllers
{
    public class WebSocketController : Controller
    {
        static ConcurrentDictionary<int, WebSocket> Websockets = new ConcurrentDictionary<int, WebSocket>();
        public IActionResult Index()
        {
            return View();
        }
        [Route("/ws")]
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

        private async Task ProcessWebSocket(WebSocket webSocket)
        {
             Websockets.TryAdd(webSocket.GetHashCode(), webSocket);
            var buffer = new byte[1024 * 4];
            var res = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer),CancellationToken.None);
            var userName = "anoymous";
            while (!res.CloseStatus.HasValue)
            {
                var cmd = Encoding.UTF8.GetString(buffer, 0, res.Count);
                if(!string.IsNullOrEmpty(cmd))
                {
                    if (cmd.StartsWith("/User")) userName = cmd.Substring(6);
                    else Broadcast($"{userName} say : {cmd}");
                }
                res = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                await webSocket.CloseAsync(res.CloseStatus.Value, res.CloseStatusDescription,CancellationToken.None);
                Websockets.TryRemove(webSocket.GetHashCode(), out var removed);
                Broadcast($"{userName} lest the room");
            } 
        }

        private void Broadcast(string message)
        {
            var buff = Encoding.UTF8.GetBytes($"{message} at {DateTime.Now}");
            var data = new ArraySegment<byte>(buff, 0, buff.Length);
            Parallel.ForEach(Websockets.Values, async (WebSocket) =>
            {
                if(WebSocket.State == WebSocketState.Open) 
                    await WebSocket.SendAsync(data, WebSocketMessageType.Text, true, CancellationToken.None);
            });
        }
    }
}
