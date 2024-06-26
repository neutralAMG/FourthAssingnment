using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForthAssignment.Core.Aplication.Models.Comment
{
    public class CommentSaveModel
    {
		public Guid Id { get; set; }
		public Guid CommentRespondingTo { get; set; }

        [Required(ErrorMessage = "Comment text content is a require fild")]
        public string CommentText { get; set; }
		public string? CommentImgUrl { get; set; }

		public Guid PostId { get; set; }
		public Guid UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public IFormFile? File { get; set; }
    }
}
