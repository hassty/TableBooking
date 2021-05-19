using Core.Contracts;
using Core.Entities.Users;
using DataAccess.Database;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Xunit;

namespace Core.Tests
{
    public class UserRepositoryTests
    {
        private readonly IUserRepository _userRepository;

        public UserRepositoryTests()
        {
            _userRepository = GetInMemoryRepository();
        }

        private IUserRepository GetInMemoryRepository()
        {
            var options = new DbContextOptionsBuilder<TableBookingContext>()
                .UseInMemoryDatabase(nameof(UserRepositoryTests))
                .Options;

            var context = new TableBookingContext(options);
            return new UserRepository(context);
        }

        [Fact]
        public void Add_ShouldAddOneUniqueUser()
        {
            var user = new UserEntity { Username = "gp" };
            var initialCount = (_userRepository.GetAll() as List<UserEntity>).Count;

            _userRepository.Add(user);
            _userRepository.SaveChanges();

            var allUsers = _userRepository.GetAll() as List<UserEntity>;
            Assert.True(allUsers.Count == initialCount + 1);
            Assert.Contains(user, allUsers);
        }

        [Fact]
        public void Add_ShouldFailAddingUsersWithSameUsername()
        {
            var user1 = new UserEntity { Username = "user" };
            var user2 = new UserEntity { Username = "user" };

            Assert.Throws<InvalidOperationException>(() =>
            {
                _userRepository.Add(user1);
                _userRepository.Add(user2);
            });
        }

        [Fact]
        public void Add_ShouldWorkWithAddRange()
        {
            var user1 = new UserEntity { Username = "multiple1" };
            var user2 = new UserEntity { Username = "multiple2" };
            var user3 = new UserEntity { Username = "multiple3" };
            var user4 = new UserEntity { Username = "multiple4" };
            var initialCount = (_userRepository.GetAll() as List<UserEntity>).Count;

            _userRepository.Add(user1);
            _userRepository.SaveChanges();
            _userRepository.AddRange(new List<UserEntity> { user2, user3 });
            _userRepository.Add(user4);
            _userRepository.SaveChanges();

            var allUsers = _userRepository.GetAll() as List<UserEntity>;
            Assert.True(allUsers.Count == initialCount + 4);
            Assert.Contains(user1, allUsers);
            Assert.Contains(user2, allUsers);
            Assert.Contains(user3, allUsers);
            Assert.Contains(user4, allUsers);
        }

        [Fact]
        public void AddRange_ShouldAddMultipleUniqueUsers()
        {
            var user1 = new UserEntity { Username = "user1" };
            var user2 = new UserEntity { Username = "user2" };
            var users = new List<UserEntity> { user1, user2 };
            var initialCount = (_userRepository.GetAll() as List<UserEntity>).Count;

            _userRepository.AddRange(users);
            _userRepository.SaveChanges();

            var allUsers = _userRepository.GetAll() as List<UserEntity>;
            Assert.True(allUsers.Count == initialCount + 2);
            Assert.Equal(user1, _userRepository.GetUserWithUsername(user1.Username));
            Assert.Equal(user2, _userRepository.GetUserWithUsername(user2.Username));
        }

        [Fact]
        public void Get_ShouldReturnNullIfUsernameNotFound()
        {
            Assert.Null(_userRepository.Get(1488));
        }

        [Fact]
        public void GetUserWithUserName_ShoudReturnNullIfUsernameNotFound()
        {
            var username = "falshiuka";

            Assert.Null(_userRepository.GetUserWithUsername(username));
        }

        [Fact]
        public void GetUserWithUserName_ShoudReturnUserIfSuchUsernameExists()
        {
            var username = "existingUsername";
            var user = new UserEntity { Username = username };

            _userRepository.Add(user);
            _userRepository.SaveChanges();

            Assert.Equal(user, _userRepository.GetUserWithUsername(username));
        }

        [Fact]
        public void Remove_ShouldNotRemoveUserIfUsernameNotFound()
        {
            var user = new UserEntity { Username = "fake" };
            var initialCount = (_userRepository.GetAll() as List<UserEntity>).Count;

            _userRepository.Remove(user);
            _userRepository.SaveChanges();

            var allUsers = _userRepository.GetAll() as List<UserEntity>;
            Assert.True(allUsers.Count == initialCount);
            Assert.DoesNotContain(user, allUsers);
        }

        [Fact]
        public void Remove_ShouldRemoveSingleUser()
        {
            var user = new UserEntity { Username = "delet this" };
            var initialCount = (_userRepository.GetAll() as List<UserEntity>).Count;

            _userRepository.Add(user);
            _userRepository.SaveChanges();
            _userRepository.Remove(user);
            _userRepository.SaveChanges();

            var allUsers = _userRepository.GetAll() as List<UserEntity>;
            Assert.True(allUsers.Count == initialCount);
            Assert.DoesNotContain(user, allUsers);
        }

        [Fact]
        public void RemoveAll_ShouldRemoveAllUsers()
        {
            _userRepository.RemoveAll();
            _userRepository.SaveChanges();

            Assert.Empty(_userRepository.GetAll());
        }

        [Fact]
        public void RemoveRange_ShoudNotRemoveUserIfUsernameNotFound()
        {
            var user1 = new UserEntity { Username = "remove1" };
            var user2 = new UserEntity { Username = "remove2" };
            var initialCount = (_userRepository.GetAll() as List<UserEntity>).Count;

            _userRepository.Add(user1);
            _userRepository.SaveChanges();
            _userRepository.RemoveRange(new List<UserEntity> { user1, user2 });
            _userRepository.SaveChanges();

            var allUsers = _userRepository.GetAll() as List<UserEntity>;
            Assert.True(allUsers.Count == initialCount);
            Assert.DoesNotContain(user1, allUsers);
            Assert.DoesNotContain(user2, allUsers);
        }

        [Fact]
        public void RemoveRange_ShouldRemoveMultipleUsers()
        {
            var user1 = new UserEntity { Username = "remove1" };
            var user2 = new UserEntity { Username = "remove2" };
            var initialCount = (_userRepository.GetAll() as List<UserEntity>).Count;

            _userRepository.AddRange(new List<UserEntity> { user1, user2 });
            _userRepository.SaveChanges();
            _userRepository.RemoveRange(new List<UserEntity> { user1, user2 });
            _userRepository.SaveChanges();

            var allUsers = _userRepository.GetAll() as List<UserEntity>;
            Assert.True(allUsers.Count == initialCount);
            Assert.DoesNotContain(user1, allUsers);
            Assert.DoesNotContain(user2, allUsers);
        }
    }
}