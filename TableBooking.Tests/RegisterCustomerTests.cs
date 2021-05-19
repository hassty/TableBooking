using Core.Contracts;
using Core.Contracts.DataAccess;
using Core.UseCases;
using System;
using System.Collections.Generic;
using System.Text;
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
            Assert.True(_customerRepository.ContainsCustomerWithUsername(customer.Username));
        }
    }
}
