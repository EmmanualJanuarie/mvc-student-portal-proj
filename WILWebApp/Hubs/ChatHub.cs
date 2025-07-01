using Microsoft.AspNetCore.SignalR;
namespace WILWebApp.Hubs
{
    public class ChatHub:Hub
    {
        private static List<string> _connectedUsers = new List<string>();

        public override async Task OnConnectedAsync()
        {
            // Add the user to the connected users list
            _connectedUsers.Add(Context.ConnectionId);
            await Clients.All.SendAsync("UpdateUser List", _connectedUsers);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // Remove the user from the connected users list
            _connectedUsers.Remove(Context.ConnectionId);
            await Clients.All.SendAsync("UpdateUser List", _connectedUsers);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.Hour, DateTime.Now.Minute);
        }
    }
}
