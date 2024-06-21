
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ForthAssignment.Core.Aplication.Utils.UserSessionHandler
{
	public static class UserSessionHandler
	{
		public static T Get<T>(this ISession session, string key)
		{
			var sessionValue = session.GetString(key);
			return JsonConvert.DeserializeObject<T>(sessionValue);
		}
		public static void Set<T>(this ISession session, string key, T value)
		{
			var sessionValue = JsonConvert.SerializeObject(value);
			session.SetString(key, sessionValue);
		}
	}
}
