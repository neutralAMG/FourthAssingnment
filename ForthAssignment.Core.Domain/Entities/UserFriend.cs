using ForthAssignment.Core.Domain.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForthAssignment.Core.Domain.Entities
{
	public class UserFriend : BaseEntity
	{
		public Guid UserId { get; set; }
		public Guid UserFriendId { get; set; }

		[ForeignKey("UserId")]
		public User User { get; set; }
		[ForeignKey("UserFriendId")]
		public User UsersFriend { get; set; }
	}
}
