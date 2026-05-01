using System.Security.Cryptography;

namespace CarsLogWorkigVS.Database
{
    public static class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100_000;
        private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;

        public static string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);

            var result = new byte[SaltSize + HashSize];
            Buffer.BlockCopy(salt, 0, result, 0, SaltSize);
            Buffer.BlockCopy(hash, 0, result, SaltSize, HashSize);

            return Convert.ToBase64String(result);
        }

        public static bool Verify(string password, string storedHash)
        {
            try
            {
                var bytes = Convert.FromBase64String(storedHash);
                if (bytes.Length != SaltSize + HashSize) return false;

                var salt = new byte[SaltSize];
                var expectedHash = new byte[HashSize];
                Buffer.BlockCopy(bytes, 0, salt, 0, SaltSize);
                Buffer.BlockCopy(bytes, SaltSize, expectedHash, 0, HashSize);

                var actualHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);
                return CryptographicOperations.FixedTimeEquals(expectedHash, actualHash);
            }
            catch
            {
                return false;
            }
        }
    }
}
