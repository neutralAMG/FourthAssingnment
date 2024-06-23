

using ForthAssignment.Core.Aplication.Models.User;
using ForthAssignment.Core.Aplication.Utils.UserSessionHandler;
using Microsoft.AspNetCore.Http;

namespace ForthAssignment.Core.Aplication.Utils.UserAuth
{
	public class UserAuth : IUserAuth
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserModel? _currentUser;
		public UserAuth(IHttpContextAccessor httpContextAccessor)
        {
			_httpContextAccessor = httpContextAccessor;
			_currentUser = _httpContextAccessor.HttpContext.Session.Get<UserModel>("user");
		}
        public bool IsUserActivated()
		{
			if(_currentUser is null) return false;
			return _currentUser.IsActive;
		}

		public bool IsUserLogin()
		{
			return _currentUser != null;
		}
	}
}
