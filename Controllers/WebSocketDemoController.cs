using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using RealTimeAppDemo.Models;
using RealTimeAppDemo.Services;

namespace RealTimeAppDemo.Controllers
{
	[Route("websocket")]
	[ApiController]
	public class WebSocketDemoController : ControllerBase
	{
		/// <summary>
		/// The memory storage for the list of connectionIds
		/// </summary>
		private static readonly List<string> ConnectionIds = new List<string>();

		private readonly IWebSocketService _webSocketService;

		public WebSocketDemoController(IWebSocketService webSocketService)
		{
			_webSocketService = webSocketService;
		}

		/// <summary>
		/// Having a new connection from client.
		/// </summary>
		[HttpPost("connect")]
		[AllowAnonymous]
		public void Connect([FromBody] ConnectionModel connection)
		{
			// Store connectionId in memory data
			ConnectionIds.Add(connection.Id);

			// The connection.UserId can be used to identify which user's using that connectionId
		}

		/// <summary>
		/// There is any disconnected connection from the client.
		/// </summary>
		[HttpPost("disconnect")]
		[AllowAnonymous]
		public void Disconnect([FromBody] ConnectionModel connection)
		{
			// Remove connectionId out of memory data
			ConnectionIds.Remove(connection.Id);
		}

		/// <summary>
		/// The clients send messages to the server
		/// </summary>
		[HttpPost("message")]
		[AllowAnonymous]
		public async Task SendMessage([FromBody] TextMessageModel input)
		{
			// Get the connectionIds of other users in the chat room
			var connectionIds = ConnectionIds.Where(id => id != input.ConnectionId).ToList();

			// Sends the message to all connected clients using the new API Gateway Management API 
			// excepts the current connection Id
			await _webSocketService.SendMessage(connectionIds, "newMessage", input.Message);
		}
	}
}
