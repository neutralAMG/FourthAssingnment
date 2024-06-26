using ForthAssignment.Core.Aplication.Models.Comment;
using ForthAssignment.Core.Aplication.Models.Post;
using ForthAssignment.Core.Aplication.Models.UserFriend;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ForthAssignment.Core.Aplication.Models.User
{
    public class UserModel
    {
		public Guid Id {  get; set; }
		[Required(ErrorMessage =  "Name is a require fild")]
		public string Name { get; set; }
        [Required(ErrorMessage = "Last Name is a require fild")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "UserName is a require fild")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Phone Number is a require fild")]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email is a require fild")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Profile image is a require fild")]
        public string ProfileImageUrl { get; set; }

        [Required(ErrorMessage = "Password is a require fild")]
        public string Password { get; set; }

		public bool IsActive { get; set; }

		[JsonIgnore]
		public IList<UserModel>? UserFriends { get; set; }

		[JsonIgnore]
		public IList<UserFriendSaveModel>? FriendsOfthUser { get; set; }
		public IList<PostModel>? UserPosts { get; set; }
		public IList<CommentModel>? UserComments { get; set; }
	}
}
