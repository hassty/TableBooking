using Core.Contracts.DataAccess;
using Core.Entities.Users;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Core.UseCases
{
    public class UserAuthorizationInteractor
    {
        private readonly ICustomerRepository _customerRespository;

        public UserAuthorizationInteractor(ICustomerRepository customerRespository)
        {
            _customerRespository = customerRespository;
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();

            var asBytes = Encoding.Default.GetBytes(password);
            var hashed = sha.ComputeHash(asBytes);

            return Convert.ToBase64String(hashed);
        }

        public bool CheckLoginCredentials(string username, string password)
        {
            var registeredUser = _customerRespository.GetCustomerWithUsername(username);
            if (registeredUser != null)
            {
                var saltedPassword = $"{registeredUser.Salt}{password}";
                var hashedPassword = HashPassword(saltedPassword);
                return registeredUser.PasswordHash == hashedPassword;
            }

            return false;
        }

        public bool Register(string username, string password)
        {
            if (String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            if (_customerRespository.GetCustomerWithUsername(username) == null)
            {
                var rng = new Random();
                var salt = rng.Next();
                var saltedPassword = $"{salt}{password}";
                var hashedPassword = HashPassword(saltedPassword);
                _customerRespository.Add(
                    new CustomerEntity
                    {
                        Username = username,
                        PasswordHash = hashedPassword,
                        Salt = salt
                    }
                );
                _customerRespository.SaveChanges();
                return true;
            }

            return false;
        }

    }
}