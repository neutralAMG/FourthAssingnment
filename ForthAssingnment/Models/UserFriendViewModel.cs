using ForthAssignment.Core.Aplication.Models.User;
using ForthAssignment.Core.Aplication.Models.UserFriend;

namespace ForthAssingnment.Presentation.WepApp.Models
{
    public class UserFriendViewModel
    {

        public IList<UserModel> Users { get; set; }
        public IList<UserFriendSaveModel> UsersFriends { get; set; }

    }
}
