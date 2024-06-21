using ForthAssignment.Core.Aplication.Models.User;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForthAssignment.Core.Aplication.Models.UserFriend
{
    public class UserFriendModel
    {

		public Guid Id { get; set; }	

		public UserModel User { get; set; }
	
		public UserModel UsersFriend { get; set; }
	}
}
