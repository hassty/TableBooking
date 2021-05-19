﻿using Core.Contracts;
using Core.Contracts.DataAccess;
using Core.Exceptions;
using Core.UseCases;
using System.Linq;
using TableBooking.Models;
using Xunit;

namespace Core.Tests
{
    public class RegisterCustomerTests
    {
        private readonly RegisterCustomer _registerCustomer;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPasswordProtectionStrategy _passwordProtectionStrategy;

        public RegisterCustomerTests()
        {
            var databaseFixture = new DatabaseFixture(nameof(RegisterCustomerTests));

            _customerRepository = databaseFixture.CustomerRepository;
            _passwordProtectionStrategy = new Sha256HashPasswordStrategy();

            _registerCustomer = new RegisterCustomer(_customerRepository, _passwordProtectionStrategy);
        }

        [Fact]
        public void Register_ShouldRegisterNewUniqueUser()
        {
            var customer = new CustomerModel
            {
                Username = "unique customer",
                Password = "1488",
                Email = "email1"
            };

            _registerCustomer.Register(customer);
            Assert.True(_customerRepository.ContainsUserWithUsername(customer.Username));
        }

        [Fact]
        public void Register_ShouldThrowExceptionIfUsernameAlreadyExists()
        {
            var customer1 = new CustomerModel
            {
                Username = "existing username",
                Password = "1337"
            };
            var customer2 = new CustomerModel
            {
                Username = "existing username",
                Password = "420"
            };
            var initialCount = _customerRepository.GetAll().ToList().Count;

            Assert.Throws<UserAlreadyExistsException>(() =>
            {
                _registerCustomer.Register(customer1);
                _registerCustomer.Register(customer2);
            });

            var allCustomers = _customerRepository.GetAll().ToList();
            Assert.True(_customerRepository.ContainsUserWithUsername(customer1.Username));
            Assert.True(allCustomers.Count == initialCount + 1);
        }
    }
}
