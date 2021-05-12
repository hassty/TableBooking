using AutoMapper;
using Core.Contracts;
using Core.Entities.Users;
using DataAccess;
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
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserRepositoryTests()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<DataAccessMappingProfile>());
            _mapper = new Mapper(configuration);

            _userRepository = GetInMemoryRepository();
        }

        private IUserRepository GetInMemoryRepository()
        {
            var options = new DbContextOptionsBuilder<TableBookingContext>()
                .UseInMemoryDatabase(nameof(UserRepositoryTests))
                .Options;

            var context = new TableBookingContext(options);
            return new UserRepository(context, _mapper);
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

        [Fact(Skip = "UserEntity doesn't have Id")]
        public void Get_ShouldReturnUserWithId()
        {
            var user = new UserEntity { Username = "id test" };

            _userRepository.Add(user);
            _userRepository.SaveChanges();

            Assert.Equal(user, _userRepository.Get(10));
        }

        [Fact]
        public void GetUserWithUserName_ShoudReturnNullIfThereIsNoSuchUsername()
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

        [Fact(Skip = "Ef Core instance can not be tracked")]
        public void Remove_ShouldRemoveSingleUser()
        {
            var user = new UserEntity { Username = "remove" };
            var initialCount = (_userRepository.GetAll() as List<UserEntity>).Count;

            _userRepository.Add(user);
            _userRepository.SaveChanges();
            _userRepository.Remove(user);
            _userRepository.SaveChanges();

            var allUsers = _userRepository.GetAll() as List<UserEntity>;
            Assert.True(allUsers.Count == initialCount);
        }

        [Fact(Skip = "Ef Core instance can not be tracked")]
        public void RemoveRange_ShouldRemoveMultipleUsers()
        {
            var user1 = new UserEntity { Username = "removing1" };
            var user2 = new UserEntity { Username = "removing2" };
            var initialCount = (_userRepository.GetAll() as List<UserEntity>).Count;

            _userRepository.AddRange(new List<UserEntity> { user1, user2 });
            _userRepository.SaveChanges();
            _userRepository.RemoveRange(new List<UserEntity> { user1, user2 });
            _userRepository.SaveChanges();

            var allUsers = _userRepository.GetAll() as List<UserEntity>;
            Assert.True(allUsers.Count == initialCount);
        }
    }
}