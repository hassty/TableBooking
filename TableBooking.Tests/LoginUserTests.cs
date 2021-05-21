using Core.Contracts.DataAccess;
using Core.Entities.Users;
using Core.Exceptions;
using Core.UseCases;
using System;
using System.Collections.Generic;
using System.Text;
using WpfUI.Models;
using Xunit;

namespace Core.Tests
{
    public class LoginUserTests
    {
        private readonly RegisterCustomer _registerCustomer;
        private readonly RegisterAdmin _registerAdmin;
        private readonly LoginUser _loginUser;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAdminRepository _adminRepository;

        public LoginUserTests()
        {
            var databaseFixture = new DatabaseFixture(nameof(LoginUserTests));

            _customerRepository = databaseFixture.CustomerRepository;
            _adminRepository = databaseFixture.AdminRepository;

            var passwordProtectionStrategy = new Sha256HashPasswordStrategy();
            _registerCustomer = new RegisterCustomer(_customerRepository, passwordProtectionStrategy);
            _registerAdmin = new RegisterAdmin(_adminRepository, passwordProtectionStrategy);

            _loginUser = new LoginUser(_customerRepository, _adminRepository, passwordProtectionStrategy);
        }

        [Theory]
        [InlineData("gp", "1488")]
        [InlineData("registered customer", "1337")]
        [InlineData("killer joker", "69420")]
        public void Login_ShouldReturnCustomerIfEnteredMatchingCredentials(string username, string password)
        {
            var customer = new CustomerModel { Username = username, Password = password };

            _registerCustomer.Register(customer);
            var user = _loginUser.Login(username, password);

            Assert.Equal(user, _customerRepository.GetUserWithUsername(username));
        }

        [Fact]
        public void Login_ShouldReturnAdminIfEnteredMatchingCredentials()
        {
            var username = "registered admin";
            var password = "password";
            var admin = new AdminModel { Username = username, Password = password };

            _registerAdmin.Register(admin);
            var user = _loginUser.Login(username, password);

            Assert.Equal(user, _adminRepository.GetUserWithUsername(username));
        }

        [Fact]
        public void Loign_ShouldFailIfSuchUserNotExists()
        {
            Assert.Throws<InvalidCredentialsException>(() =>
            {
                _loginUser.Login("unregistered user", "1337");
            });
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("fake", "")]
        [InlineData("", "fake")]
        [InlineData(null, "fake")]
        [InlineData(null, null)]
        [InlineData("", null)]
        public void Login_ShouldFailIfEnteredInvalidCredentials(string username, string password)
        {
            Assert.Throws<InvalidCredentialsException>(() =>
            {
                _loginUser.Login(username, password);
            });
        }
    }
}
