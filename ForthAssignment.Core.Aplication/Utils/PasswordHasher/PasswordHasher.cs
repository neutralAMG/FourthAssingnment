
using System.Security.Cryptography;

namespace ForthAssignment.Core.Aplication.Utils.PasswordHasher
{
	public class PasswordHasher : IPasswordHasher
	{
		private const int SaltSize = 128 / 8;
		private const int KeySize = 256 / 8;
		public const int Iterations = 1000;
		private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
		private const char Delimiter = ';';

		public string HashPassword(string password)
		{
			byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
			byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _hashAlgorithmName, KeySize);

			return string.Join(Delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
		}

		public bool VerifyPassword(string hashedPassword, string inputPassword)
		{
			var elements = hashedPassword.Split(Delimiter);
			var salt = Convert.FromBase64String(elements[0]);
			var hash = Convert.FromBase64String(elements[1]);
			var hashInput = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, Iterations, _hashAlgorithmName, KeySize);

			return CryptographicOperations.FixedTimeEquals(hash, hashInput);
		}
	}
}
