using ForthAssignment.Core.Domain.Core;

namespace ForthAssignment.Core.Domain.Entities
{
	public class User : BaseEntity
	{
		public string Name { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public string? ProfileImageUrl { get; set; }
		public string Password { get; set; }
		public bool IsActive { get; set; }

		public IList<UserFriend> UserFriends { get; set; } 
		public IList<Post> UserPosts { get; set; } 
		public IList<Comment> UserComments { get; set; } 
	//	public IList<PostLike> UserPostLikes { get; set; }
	//	public IList<CommentLike> UserCommentLikes { get; set; }
	}
}
