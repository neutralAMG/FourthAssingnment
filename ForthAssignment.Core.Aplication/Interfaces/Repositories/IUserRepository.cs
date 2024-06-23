

using ForthAssignment.Core.Aplication.Core;
using ForthAssignment.Core.Domain.Entities;

namespace ForthAssignment.Core.Aplication.Interfaces.Repositories
{
	public interface IUserRepository : IBaseRepository<User>
	{
		Task<User> Login(string user);
		Task ActivateUser(Guid id);
		Task<User> GetUserByUserName(string UserName);
		Task<User> ChangePassword(string username, string NewPassword);

	}
}
