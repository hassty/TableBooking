using Core.Contracts;
using Core.Contracts.DataAccess;
using Core.Contracts.Dto;
using Core.Entities.Users;
using Core.Exceptions;
using System;

namespace Core.UseCases
{
    public class RegisterCustomer
    {
        private readonly ICustomerRepository _customerRespository;
        private readonly IPasswordProtectionStrategy _passwordProtectionStrategy;

        public RegisterCustomer(
            ICustomerRepository customerRespository,
            IPasswordProtectionStrategy passwordProtectionStrategy
        )
        {
            _customerRespository = customerRespository;
            _passwordProtectionStrategy = passwordProtectionStrategy;
        }

        private (int, string) HashAndSaltPassword(string password)
        {
            var rng = new Random();
            var salt = rng.Next();
            var saltedPassword = $"{salt}{password}";

            return (salt, _passwordProtectionStrategy.GetProtectedPassword(saltedPassword));
        }

        /// <exception cref="UserAlreadyExistsException"></exception>
        public void Register(ICustomerDto customer)
        {
            if (_customerRespository.ContainsCustomerWithUsername(customer.Username))
            {
                throw new UserAlreadyExistsException("User with this username already exists");
            }

            var newCustomer = customer.ToEntity();
            (newCustomer.Salt, newCustomer.PasswordHash) = HashAndSaltPassword(customer.Password);

            _customerRespository.Add(newCustomer);
            _customerRespository.SaveChanges();
        }
    }
}
