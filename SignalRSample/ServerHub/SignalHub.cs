

using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace SignalRSample.ServerHub
{
    public class SignalHub : Hub
    {
        public static List<HubConnections> Connections = new List<HubConnections>();

        public async Task AllertToAll(string clientMessage)
        {
            await Clients.All.SendAsync("AlertEvent", clientMessage);
        }

        public override Task OnConnectedAsync()
        {
            var ConnectionId = Context.ConnectionId;

            //در مثالهای واقعی میتوانیم یوزر آیدی را از توکن کاربر دریافت کنیم
            //var UserId = Guid.NewGuid().ToString();
            string UserId = Context.User.Identity.Name;

            Connections.Add(new HubConnections
            {
                ConnectionId = ConnectionId,
                UserId = UserId,
            });
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var ConnectionId = Context.ConnectionId;

            Connections.RemoveAll(a => a.ConnectionId == ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string input)
        {
            var inputObject = JsonConvert.DeserializeObject<SendMessageInput>(input);
            var destinationConnectionId = Connections.FirstOrDefault(a => a.UserId == inputObject.DestinationUserId).ConnectionId;
            await Clients.Client(destinationConnectionId).SendAsync("SendMessageEvent",inputObject.Message);
        }
    }
    public class HubConnections
    {
        public string UserId { set; get; }
        public string ConnectionId { set; get; }
    }
    public class SendMessageInput
    {
        public string DestinationUserId { set; get; }
        public string Message { set; get; }
    }
}
