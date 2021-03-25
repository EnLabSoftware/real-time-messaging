namespace RealTimeAppDemo.Models
{
	public class WebSocketMessageModel
	{
		public WebSocketMessageModel(string action, object payload)
		{
			Action = action;
			Payload = payload;
		}

		public string Action { get; set; }
		public object Payload { get; set; }
	}
}
