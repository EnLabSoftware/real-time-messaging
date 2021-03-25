using Amazon.ApiGatewayManagementApi;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Amazon.ApiGatewayManagementApi.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RealTimeAppDemo.Models;

namespace RealTimeAppDemo.Services
{
	public class WebSocketService : IWebSocketService
	{
		private const string webSocketEndpoint = "your_websocket_endpoint"; // your WebSocket endpoint
		private const string awsAccessKeyId = "your_awsAccessKeyId"; // your awsAccessKeyId
		private const string awsSecretAccessKey = "your_awsSecretAccessKey"; // your awsSecretAccessKey

		private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
		{
			ContractResolver = new CamelCasePropertyNamesContractResolver()
		};

		private static AmazonApiGatewayManagementApiClient _webSocketApiClient;

		public WebSocketService()
		{
		}

		private static AmazonApiGatewayManagementApiClient WebSocketApiClient
		{
			get
			{
				if (_webSocketApiClient != null)
				{
					return _webSocketApiClient;
				}

				var apiConfig = new AmazonApiGatewayManagementApiConfig { ServiceURL = webSocketEndpoint };

				return _webSocketApiClient = new AmazonApiGatewayManagementApiClient(awsAccessKeyId, awsSecretAccessKey, apiConfig);
			}
		}

		public async Task SendMessage(IList<string> connectionIds, string action, object payload)
		{
			foreach (var connectionId in connectionIds)
			{
				await PostToConnection(connectionId, action, payload);
			}
		}

		private static async Task PostToConnection(string connectionId, string action, object payload)
		{
			var json = JsonConvert.SerializeObject(new WebSocketMessageModel(action, payload), JsonSettings);

			await WebSocketApiClient.PostToConnectionAsync(new PostToConnectionRequest
			{
				ConnectionId = connectionId,
				Data = new MemoryStream(Encoding.UTF8.GetBytes(json))
			});
		}
	}
}
