
using ForthAssignment.Core.Aplication.Core;
using ForthAssignment.Core.Domain.Entities;

namespace ForthAssignment.Core.Aplication.Interfaces.Repositories
{
	public interface IPostRepository : IBaseRepository<Post>, IDelete<Post>
	{
		Task<List<Post>> GetAll(Guid id);
		Task<List<Post>> GetAllFrindsPosts(List<User> UsersFriends, Guid userId);
	}
}
