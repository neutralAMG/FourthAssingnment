
using ForthAssignment.Core.Aplication.Core;
using ForthAssignment.Core.Aplication.Models.Comment;
using ForthAssignment.Core.Domain.Entities;


namespace ForthAssignment.Core.Aplication.Interfaces.Contracts
{
	public interface ICommentService : IBaseService<CommentSaveModel, CommentModel, Comment>
	{
		Task<Result<CommentSaveModel>> RespondComment(CommentSaveModel comment, Guid CommentBeingRRespondedToId);
	}
}
