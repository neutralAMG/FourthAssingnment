

using ForthAssignment.Core.Aplication.Models.User;
using ForthAssignment.Core.Aplication.Utils.UserSessionHandler;
using Microsoft.AspNetCore.Http;

namespace ForthAssignment.Core.Aplication.Utils.UserAuth
{
	public class UserAuth : IUserAuth
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		public UserAuth(IHttpContextAccessor httpContextAccessor)
        {
			_httpContextAccessor = httpContextAccessor;
		}
        public bool IsUserActivated()
		{
			if(_httpContextAccessor.HttpContext.Session.Get<UserModel>("user") is null) return false;
			return _httpContextAccessor.HttpContext.Session.Get<UserModel>("user").IsActive;
		}

		public bool IsUserLogin()
		{
			return _httpContextAccessor.HttpContext.Session.Get<UserModel>("user") != null? true : false;
		}
	}
}
