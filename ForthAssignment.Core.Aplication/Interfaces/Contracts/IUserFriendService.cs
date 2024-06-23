

using ForthAssignment.Core.Aplication.Core;
using ForthAssignment.Core.Aplication.Models.User;
using ForthAssignment.Core.Aplication.Models.UserFriend;
using ForthAssignment.Core.Domain.Entities;

namespace ForthAssignment.Core.Aplication.Interfaces.Contracts
{
	public interface IUserFriendService : IBaseService<UserFriendSaveModel,UserFriendModel, UserFriend>, IDeleteService<UserFriendModel>
	{
		Task<Result<List<UserModel>>> GetUserFriends(Guid id);
		Task<Result<List<UserModel>>> GetUserPosibleFriends(Guid id);
	}
}
