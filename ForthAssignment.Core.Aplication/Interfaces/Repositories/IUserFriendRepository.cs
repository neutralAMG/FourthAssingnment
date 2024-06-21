using ForthAssignment.Core.Aplication.Core;
using ForthAssignment.Core.Aplication.Models.User;
using ForthAssignment.Core.Domain.Entities;
namespace ForthAssignment.Core.Aplication.Interfaces.Repositories
{
	public interface IUserFriendRepository : IBaseRepository<UserFriend>, IDelete<UserFriend>
	{
		Task<List<User>> GetUserFriends(Guid id);
		Task<List<User>> GetUserPosibleFriends(Guid id);
	}
}
