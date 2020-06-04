using System;
using System.Linq;

namespace UserManagement.Application.Helpers
{
    public static class Cryptography
    {
        private const string valueNullOrEmptyMessage = "Value cannot be null or empty.";
        private const string valueNullOrWhitespaceMessage = "Value cannot be empty or whitespace only string.";
        private const int expectedHashLength = 64;
        private const int expectedSaltLength = 128;

        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(valueNullOrEmptyMessage, nameof(password));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException(valueNullOrWhitespaceMessage, nameof(password));

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(valueNullOrEmptyMessage, nameof(password));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException(valueNullOrWhitespaceMessage, nameof(password));

            if (storedHash.Length != expectedHashLength) throw new ArgumentException("Invalid length of password hash (" +
                expectedHashLength +
                " bytes expected).", nameof(storedHash));

            if (storedSalt.Length != expectedSaltLength) throw new ArgumentException("Invalid length of password salt (" +
                expectedSaltLength +
                " bytes expected).", nameof(storedSalt));

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(storedHash);
            }
        }
    }
}
