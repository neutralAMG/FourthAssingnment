

namespace ForthAssignment.Core.Aplication.Utils.PasswordHasher
{
	public interface IPasswordHasher
	{
		string HashPassword(string password);

		bool VerifyPassword(string hashedPassword, string inputPassword);
	}
}
