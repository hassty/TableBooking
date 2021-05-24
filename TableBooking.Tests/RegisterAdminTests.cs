﻿using Core.Contracts;
using Core.Contracts.DataAccess;
using Core.Exceptions;
using Core.UseCases;
using System.Linq;
using WpfUI.Models;
using Xunit;

namespace Core.Tests
{
    public class RegisterAdminTests
    {
        private readonly RegisterAdmin _registerAdmin;
        private readonly IAdminRepository _adminRepository;
        private readonly IPasswordProtectionStrategy _passwordProtectionStrategy;
        public RegisterAdminTests()
        {
            var databaseFixture = new DatabaseFixture(nameof(RegisterAdminTests));

            _adminRepository = databaseFixture.AdminRepository;
            _passwordProtectionStrategy = new Sha256HashPasswordStrategy();

            _registerAdmin = new RegisterAdmin(_adminRepository, _passwordProtectionStrategy);
        }


        [Fact]
        public void Register_ShouldRegisterNewUniqueUser()
        {
            var admin = new AdminModel
            {
                Username = "unique admin",
                Password = "1488",
            };

            _registerAdmin.Register(admin);
            Assert.True(_adminRepository.ContainsUserWithUsername(admin.Username));
        }

        [Fact]
        public void Register_ShouldThrowExceptionIfUsernameAlreadyExists()
        {
            var admin1 = new AdminModel
            {
                Username = "existing username",
                Password = "1337"
            };
            var admin2 = new AdminModel
            {
                Username = "existing username",
                Password = "420"
            };
            var initialCount = _adminRepository.GetAll().ToList().Count;

            Assert.Throws<UserAlreadyExistsException>(() =>
            {
                _registerAdmin.Register(admin1);
                _registerAdmin.Register(admin2);
            });

            var allAdmins = _adminRepository.GetAll().ToList();
            Assert.True(_adminRepository.ContainsUserWithUsername(admin1.Username));
            Assert.True(allAdmins.Count == initialCount + 1);
        }
    }
}
