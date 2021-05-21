using Core.Contracts.DataAccess;
using Core.Entities.Users;
using Core.UseCases;
using DataAccess.Database;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Core.Tests
{
    public class UserAuthorizationInteractorTests
    {
        private readonly UserAuthorizationInteractor _userAuthorizationInteractor;
        private readonly ICustomerRepository _customerRespository;

        public UserAuthorizationInteractorTests()
        {
            _customerRespository = GetInMemoryRepository();
            _userAuthorizationInteractor = new UserAuthorizationInteractor(_customerRespository);
        }

        private ICustomerRepository GetInMemoryRepository()
        {
            var options = new DbContextOptionsBuilder<TableBookingContext>()
                .UseInMemoryDatabase(nameof(UserAuthorizationInteractorTests))
                .Options;

            var context = new TableBookingContext(options);
            return new CustomerRepository(context);
        }

        [Fact]
        public void CheckLoginCredentials_ShouldReturnFalseIfEnteredInvalidCredentials()
        {
            _userAuthorizationInteractor.Register("correct", "password");

            Assert.False(_userAuthorizationInteractor.CheckLoginCredentials("incorrect", "password"));
            Assert.False(_userAuthorizationInteractor.CheckLoginCredentials("correct", "1234"));
        }

        [Theory]
        [InlineData("kila", "shkila")]
        [InlineData("gp", "1488")]
        public void CheckLoginCredentials_ShouldReturnTrueIfEnteredCorrectCredentials(string username, string password)
        {
            _userAuthorizationInteractor.Register(username, password);

            Assert.True(_userAuthorizationInteractor.CheckLoginCredentials(username, password));
        }

        [Fact]
        public void Register_ShouldAddNewUser()
        {
            var username = "kila";
            var password = "shkila";
            var initialCount = (_customerRespository.GetAll().ToList()).Count;

            _userAuthorizationInteractor.Register(username, password);
            var allUsers = _customerRespository.GetAll().ToList();

            Assert.True(allUsers.Count == initialCount + 1);
            Assert.True(_userAuthorizationInteractor.CheckLoginCredentials(username, password));
        }

        [Theory]
        [InlineData("sho", "")]
        [InlineData("", "")]
        [InlineData(null, "")]
        [InlineData("", null)]
        [InlineData(null, null)]
        [InlineData(null, "shok")]
        public void Register_ShouldFailAddingUsersWithInvalidCredentials(string username, string password)
        {
            Assert.False(_userAuthorizationInteractor.Register(username, password));
        }

        [Fact]
        public void Register_ShouldFailAddingUsersWithSameUsername()
        {
            var username = "same name";
            var password1 = "password1";
            var password2 = "password2";
            var initialCount = (_customerRespository.GetAll().ToList()).Count;

            Assert.True(_userAuthorizationInteractor.Register(username, password1));
            Assert.False(_userAuthorizationInteractor.Register(username, password2));

            var allUsers = _customerRespository.GetAll().ToList();
            Assert.True(allUsers.Count == initialCount + 1);
            Assert.True(_userAuthorizationInteractor.CheckLoginCredentials(username, password1));
        }
    }
}