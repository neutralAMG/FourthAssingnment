

namespace ForthAssignment.Core.Aplication.Utils.PasswordHasher
{
	public interface IPasswordHasher
	{
		string Hash(string password);

		bool VerifyPassword(string hashedPassword, string inputPassword);
	}
}
