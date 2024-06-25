﻿using ForthAssignment.Core.Aplication.Models.Comment;
using ForthAssignment.Core.Aplication.Models.Post;
using ForthAssignment.Core.Aplication.Models.UserFriend;
using System.Text.Json.Serialization;

namespace ForthAssignment.Core.Aplication.Models.User
{
    public class UserModel
    {
		public Guid Id {  get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public string? ProfileImageUrl { get; set; }
		public string Password { get; set; }
		public bool IsActive { get; set; }

		[JsonIgnore]
		public IList<UserModel> UserFriends { get; set; }

		[JsonIgnore]
		public IList<UserModel> FriendsOfthUser { get; set; }
		public IList<PostModel> UserPosts { get; set; }
		public IList<CommentModel> UserComments { get; set; }
	}
}
