using ForthAssignment.Core.Aplication.Models.Post;
using ForthAssignment.Core.Aplication.Models.User;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForthAssignment.Core.Aplication.Models.Comment
{
    public class CommentModel
    {
		public Guid Id { get; set; }
		public Guid CommentRespondingTo { get; set; }
		public string CommentText { get; set; }
		public string? CommentImgUrl { get; set; }

		public PostModel Post { get; set; }
        public DateTime DateCreated { get; set; }
        public UserModel UserThatCommentetThis { get; set; }
        public CommentModel ParentComment { get; set; }
        public IList<CommentModel> Comments { get; set; }
    }
}
