
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForthAssignment.Core.Aplication.Models.Post
{
	public class PostSaveModel
	{
		public Guid Id { get; set; }
        [Required(ErrorMessage = "Post text content is a require fild")]
        public string PostText { get; set; }
		public string? PostImgUrl { get; set; }
        public string? VideoUrl { get; set; }
        public Guid? UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public IFormFile? File { get; set; }
    }
}
