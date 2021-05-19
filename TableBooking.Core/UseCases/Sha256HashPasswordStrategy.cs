using Core.Contracts;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Core.UseCases
{
    public class Sha256HashPasswordStrategy : IPasswordProtectionStrategy
    {
        private string GetProtectedPassword(string password)
        {
            using var sha = SHA256.Create();

            var asBytes = Encoding.Default.GetBytes(password);
            var hashed = sha.ComputeHash(asBytes);

            return Convert.ToBase64String(hashed);
        }

        public (int, string) HashAndSaltPassword(string password)
        {
            var rng = new Random();
            var salt = rng.Next();
            var saltedPassword = $"{salt}{password}";

            return (salt, GetProtectedPassword(saltedPassword));
        }
    }
}
