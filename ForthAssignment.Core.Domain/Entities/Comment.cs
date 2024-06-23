using ForthAssignment.Core.Domain.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForthAssignment.Core.Domain.Entities
{
	public class Comment : BaseDateRegisterdEntity
	{
        public Comment()
        {
            DateCreated = DateTime.Now;
        }
        public Guid CommentRespondingTo { get; set; }
		public string CommentText { get; set; }
		public string? CommentImgUrl { get; set; }

		public Guid PostId { get; set; }
		public Guid UserId { get; set; }
		[ForeignKey("PostId")]
		public Post Post { get; set; }
		[ForeignKey("UserId")]
		public User UserThatCommentetThis { get; set; }
	}
}
