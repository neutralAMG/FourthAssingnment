
using ForthAssignment.Core.Aplication.Core;
using ForthAssignment.Core.Domain.Entities;

namespace ForthAssignment.Core.Aplication.Interfaces.Repositories
{
	public interface ICommentRepository : IBaseRepository<Comment>
	{
		Task<Comment> RespondComment(Comment entity);
	}
}
