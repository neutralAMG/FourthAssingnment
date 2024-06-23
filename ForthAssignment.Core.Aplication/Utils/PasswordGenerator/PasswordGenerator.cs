
using System.Text;

namespace ForthAssignment.Core.Aplication.Utils.PasswordGenerator
{
    public class PasswordGenerator : IPasswordGenerator
    {
        public string GenereatePassword()
        {
            string ChaRToGeneratePassword = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder PasswordGenereted = new();
            Random random = new();
            for(int i = 0; i< 10; i++)
            {
                PasswordGenereted.Append( ChaRToGeneratePassword[random.Next(ChaRToGeneratePassword.Length)]);
            }

            return PasswordGenereted.ToString();
        }
    }
}
