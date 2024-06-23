
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForthAssignment.Core.Aplication.Models.Post
{
	public class PostSaveModel
	{
		public Guid Id { get; set; }
		public string PostText { get; set; }
		public string? PostImgUrl { get; set; }
		public Guid UserId { get; set; }
        public IFormFile File { get; set; }
    }
}
