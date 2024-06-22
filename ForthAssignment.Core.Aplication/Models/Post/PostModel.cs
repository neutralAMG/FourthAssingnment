

using ForthAssignment.Core.Aplication.Models.Comment;
using ForthAssignment.Core.Aplication.Models.User;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForthAssignment.Core.Aplication.Models.Post
{
	public class PostModel
	{
		public Guid Id { get; set; }
		public string PostText { get; set; }
		public string? PostImgUrl { get; set; }

		public UserModel UserThatPostThis { get; set; }
		public IList<CommentModel> Comments { get; set; }
	}
}
