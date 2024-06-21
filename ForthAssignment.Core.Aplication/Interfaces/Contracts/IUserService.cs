
using ForthAssignment.Core.Aplication.Core;
using ForthAssignment.Core.Aplication.Models.User;
using ForthAssignment.Core.Domain.Entities;

namespace ForthAssignment.Core.Aplication.Interfaces.Contracts
{
	public interface IUserService : IBaseService<UserSaveModel, UserModel, User>
	{
		Task<Result<UserModel>> Login(string userName, string password);
		Task<Result<UserModel>> ActivateUser(Guid id);
		Task<Result<UserModel>> GetUserByUserName(string UserName);

	}
}
