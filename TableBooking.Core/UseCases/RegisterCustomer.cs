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

        /// <exception cref="UserAlreadyExistsException"></exception>
        public void Register(ICustomerDto customer)
        {
            if (_customerRepository.ContainsUserWithUsername(customer.Username))
            {
                throw new UserAlreadyExistsException("User with this username already exists");
            }

            var newCustomer = customer.ToEntity();
            (newCustomer.Salt, newCustomer.PasswordHash) =
                _passwordProtectionStrategy.HashAndSaltPassword(customer.Password);

            _customerRepository.Add(newCustomer);
            _customerRepository.SaveChanges();
        }
    }
}
