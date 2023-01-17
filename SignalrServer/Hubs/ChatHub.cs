using Microsoft.AspNetCore.SignalR;

namespace SignalrServer.Hubs
{
	public class ChatHub : Hub
	{
		public async Task SendNotification(string username, string message)
		{
			
				string chats = $"<{username}>" + message;
				await Clients.All.SendAsync("showString", chats);
			
		}
	}
}