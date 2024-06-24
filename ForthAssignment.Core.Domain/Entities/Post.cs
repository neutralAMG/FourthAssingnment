using ForthAssignment.Core.Domain.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForthAssignment.Core.Domain.Entities
{
	public class Post : BaseDateRegisterdEntity
	{

        public string PostText { get; set; }
		public string? PostImgUrl { get; set; }
		public string? VideoUrl { get; set; }
		public Guid UserId { get; set; }


		[ForeignKey("UserId")]
		public User UserThatPostThis { get; set; }
		public IList<Comment> Comments { get; set; }
		// public IList<PostLike> Likes { get; set; }
	}
}
