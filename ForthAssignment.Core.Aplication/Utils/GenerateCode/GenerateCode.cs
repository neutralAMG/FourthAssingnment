
using ForthAssignment.Core.Aplication.Utils.NewFolder;
using System.Text;

namespace ForthAssignment.Core.Aplication.Utils.GenerateCode
{
    public class GenerateCode : IGenerateCode
    {
        string IGenerateCode.GenerateCode()
        {
            string ChaRToGeneratePassword = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder PasswordGenereted = new();
            Random random = new();
            for (int i = 0; i < 5; i++)
            {
                PasswordGenereted.Append(ChaRToGeneratePassword[random.Next(ChaRToGeneratePassword.Length)]);
            }

            return PasswordGenereted.ToString();
        }
    }
}
