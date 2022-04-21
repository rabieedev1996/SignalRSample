using Microsoft.AspNetCore.SignalR;
using SignalRSample.ServerHub;

namespace SignalRSample.Classes
{
    public class SampleClass
    {
        private IHubContext<SignalHub> _hubContext;


        public SampleClass(IHubContext<SignalHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public void SendBroadcastMessageSample()
        {
            _hubContext.Clients.All.SendAsync("HelloMessage", "HelloWord");
        }
        public void SendMessage(string userId,string message)
        {
            var destinationConnectionId =SignalHub.Connections.FirstOrDefault(a => a.UserId == userId).ConnectionId;

            _hubContext.Clients.Client(destinationConnectionId).SendAsync("SendMessageEvent", message);
        }
    }

   
}
