using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealTimeAppDemo.Services
{
	public interface IWebSocketService
	{
		Task SendMessage(IList<string> connectionIds, string action, object payload);
	}
}
