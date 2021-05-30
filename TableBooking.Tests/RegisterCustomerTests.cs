using Core.Contracts;
using Core.Contracts.DataAccess;
using Core.Exceptions;
using Core.Strategies;
using Core.UseCases;
using System.Linq;
using WpfUI.Dto;
using Xunit;

namespace Core.Tests
{
    public class RegisterCustomerTests
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IPasswordProtectionStrategy _passwordProtectionStrategy;
        private readonly RegisterCustomer _registerCustomer;

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
            var customer = new CustomerDto
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
            var customer1 = new CustomerDto
            {
                Username = "existing username",
                Password = "1337"
            };
            var customer2 = new CustomerDto
            {
                Username = "existing username",
                Password = "420"
            };
            var initialCount = _customerRepository.GetAll().ToList().Count;

            Assert.Throws<ItemAlreadyExistsException>(() =>
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