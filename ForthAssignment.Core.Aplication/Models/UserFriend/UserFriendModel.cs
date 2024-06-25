using ForthAssignment.Core.Aplication.Models.User;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ForthAssignment.Core.Aplication.Models.UserFriend
{
    public class UserFriendModel
    {

		public Guid Id { get; set; }
		[JsonIgnore]
		public UserModel User { get; set; }
		[JsonIgnore]
		public UserModel UsersFriend { get; set; }
	}
}
