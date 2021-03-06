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
        private readonly ICustomerRepository _customerRepository;
        private readonly IPasswordProtectionStrategy _passwordProtectionStrategy;

        public RegisterCustomer(
            ICustomerRepository customerRespository,
            IPasswordProtectionStrategy passwordProtectionStrategy
        )
        {
            _customerRepository = customerRespository;
            _passwordProtectionStrategy = passwordProtectionStrategy;
        }

        public (int, string) HashAndSaltPassword(string password)
        {
            var rng = new Random();
            var salt = rng.Next();
            var saltedPassword = $"{salt}{password}";

            var passwordHash = _passwordProtectionStrategy.GetProtectedPassword(saltedPassword);

            return (salt, passwordHash);
        }

        /// <exception cref="ItemAlreadyExistsException"></exception>
        public CustomerEntity Register(ICustomerDto customer)
        {
            if (_customerRepository.ContainsUserWithUsername(customer.Username))
            {
                throw new ItemAlreadyExistsException("User with this username already exists");
            }

            var newCustomer = customer.ToEntity();
            (newCustomer.Salt, newCustomer.PasswordHash) = HashAndSaltPassword(customer.Password);

            _customerRepository.Add(newCustomer);
            _customerRepository.SaveChanges();
            return newCustomer;
        }
    }
}