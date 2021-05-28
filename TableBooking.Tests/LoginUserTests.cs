using Core.Contracts.DataAccess;
using Core.Exceptions;
using Core.UseCases;
using WpfUI.Dto;
using Xunit;

namespace Core.Tests
{
    public class LoginUserTests
    {
        private readonly IAdminRepository _adminRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly LoginUser _loginUser;
        private readonly RegisterAdmin _registerAdmin;
        private readonly RegisterCustomer _registerCustomer;

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

        [Fact]
        public void Login_ShouldReturnAdminIfEnteredMatchingCredentials()
        {
            var username = "registered admin";
            var password = "password";
            var admin = new AdminDto { Username = username, Password = password };

            _registerAdmin.Register(admin);
            var user = _loginUser.Login(username, password);

            Assert.Equal(user, _adminRepository.GetUserWithUsername(username));
        }

        [Theory]
        [InlineData("gp", "1488")]
        [InlineData("registered customer", "1337")]
        [InlineData("killer joker", "69420")]
        public void Login_ShouldReturnCustomerIfEnteredMatchingCredentials(string username, string password)
        {
            var customer = new CustomerDto { Username = username, Password = password };

            _registerCustomer.Register(customer);
            var user = _loginUser.Login(username, password);

            Assert.Equal(user, _customerRepository.GetUserWithUsername(username));
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("fake", "")]
        [InlineData("", "fake")]
        [InlineData(null, "fake")]
        [InlineData(null, null)]
        [InlineData("", null)]
        public void Login_ShouldThrowIfIfEnteredInvalidCredentials(string username, string password)
        {
            Assert.Throws<InvalidCredentialsException>(() =>
            {
                _loginUser.Login(username, password);
            });
        }

        [Fact]
        public void Loign_ShouldThrowIfIfSuchUserNotExists()
        {
            Assert.Throws<InvalidCredentialsException>(() =>
            {
                _loginUser.Login("unregistered user", "1337");
            });
        }
    }
}