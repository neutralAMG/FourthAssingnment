﻿

using ForthAssignment.Core.Aplication.Models.Comment;
using ForthAssignment.Core.Aplication.Models.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForthAssignment.Core.Aplication.Models.Post
{
	public class PostModel
	{
		public Guid Id { get; set; }

        [Required(ErrorMessage = "Post text content is a require fild")]
        public string PostText { get; set; }

		public string? PostImgUrl { get; set; }
        public string? VideoUrl { get; set; }
        public DateTime DateCreated { get; set; }
        public UserModel? UserThatPostThis { get; set; }
		public IList<CommentModel>? Comments { get; set; }
	}
}
