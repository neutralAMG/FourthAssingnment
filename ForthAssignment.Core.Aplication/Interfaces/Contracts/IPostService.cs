
using ForthAssignment.Core.Aplication.Core;
using ForthAssignment.Core.Aplication.Models.Post;
using ForthAssignment.Core.Aplication.Models.UserFriend;
using ForthAssignment.Core.Domain.Entities;

namespace ForthAssignment.Core.Aplication.Interfaces.Contracts
{
	public interface IPostService :  IBaseService<PostSaveModel, PostModel, Post>, IDeleteService<PostModel>
	{
	    new Task<Result<List<PostModel>>> GetAll();
		Task<Result<List<PostModel>>> GetAllFrindsPosts();
	}
}
