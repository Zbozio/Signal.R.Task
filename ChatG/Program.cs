using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ChatG
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			Console.WriteLine("Enter your username:");
			var username = Console.ReadLine();
			var connection = new HubConnectionBuilder()
				//.WithUrl("http://localhost:5239/hubs/time")
				//WithUrl("http://localhost:5239/hubs/string")
				//.WithUrl("http://localhost:5239/hubs/notification")
				.WithUrl("http://localhost:5239/hubs/chat")
				//.AddMessagePackProtocol()
				.ConfigureLogging(x =>
				{
					x.AddConsole();
					x.SetMinimumLevel(LogLevel.Error);
				})
				.Build();

			connection.On<string>("showString", ShowNotification);

			await connection.StartAsync();
			//DateTime currentTime = await connection.InvokeAsync<DateTime>("GetCurrentDateTime");

			//Console.WriteLine($"Current time is {currentTime}");

			//await connection.InvokeAsync("SendStringToAllClients");
			
			var message = Console.ReadLine();
			await connection.InvokeAsync("SendNotification", username, message);

			Console.ReadKey();
		}

		private static void ShowNotification(string message)
		{
			Console.WriteLine(message);
		}
	}
}