using Core.Contracts;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Core.UseCases
{
    public class Sha256HashPasswordStrategy : IPasswordProtectionStrategy
    {
        public string GetProtectedPassword(string password)
        {
            using var sha = SHA256.Create();

            var asBytes = Encoding.Default.GetBytes(password);
            var hashed = sha.ComputeHash(asBytes);

            return Convert.ToBase64String(hashed);
        }

    }
}
