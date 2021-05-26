using Core.Contracts.DataAccess;
using Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Core.Tests
{
    public class CustomerRepositoryTests
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerRepositoryTests()
        {
            var databaseFixture = new DatabaseFixture(nameof(CustomerRepositoryTests));
            _customerRepository = databaseFixture.CustomerRepository;
        }

        [Fact]
        public void Add_ShouldAddOneUniqueUser()
        {
            var customer = new CustomerEntity { Username = "gp" };
            var initialCount = (_customerRepository.GetAll().ToList()).Count;

            _customerRepository.Add(customer);
            _customerRepository.SaveChanges();

            var allUsers = _customerRepository.GetAll().ToList();
            Assert.True(allUsers.Count == initialCount + 1);
            Assert.Contains(customer, allUsers);
        }

        [Fact]
        public void Add_ShouldThrowIfAddingUsersWithSameUsername()
        {
            var customer1 = new CustomerEntity { Username = "customer" };
            var customer2 = new CustomerEntity { Username = "customer" };

            Assert.Throws<InvalidOperationException>(() =>
            {
                _customerRepository.Add(customer1);
                _customerRepository.Add(customer2);
            });
        }

        [Fact]
        public void Add_ShouldWorkWithAddRange()
        {
            var customer1 = new CustomerEntity { Username = "multiple1" };
            var customer2 = new CustomerEntity { Username = "multiple2" };
            var customer3 = new CustomerEntity { Username = "multiple3" };
            var customer4 = new CustomerEntity { Username = "multiple4" };
            var initialCount = (_customerRepository.GetAll().ToList()).Count;

            _customerRepository.Add(customer1);
            _customerRepository.SaveChanges();
            _customerRepository.AddRange(new List<CustomerEntity> { customer2, customer3 });
            _customerRepository.Add(customer4);
            _customerRepository.SaveChanges();

            var allUsers = _customerRepository.GetAll().ToList();
            Assert.Contains(customer1, allUsers);
            Assert.Contains(customer2, allUsers);
            Assert.Contains(customer3, allUsers);
            Assert.Contains(customer4, allUsers);
        }

        [Fact]
        public void AddRange_ShouldAddMultipleUniqueUsers()
        {
            var customer1 = new CustomerEntity { Username = "customer1" };
            var customer2 = new CustomerEntity { Username = "customer2" };
            var customers = new List<CustomerEntity> { customer1, customer2 };
            var initialCount = (_customerRepository.GetAll().ToList()).Count;

            _customerRepository.AddRange(customers);
            _customerRepository.SaveChanges();

            var allUsers = _customerRepository.GetAll().ToList();
            Assert.True(allUsers.Count == initialCount + 2);
            Assert.Contains(customer1, _customerRepository.GetAll());
            Assert.Contains(customer2, _customerRepository.GetAll());
        }

        [Fact]
        public void Get_ShouldReturnNullIfUsernameNotFound()
        {
            Assert.Null(_customerRepository.Get(1488));
        }

        [Fact]
        public void GetUserWithUserName_ShoudReturnNullIfUsernameNotFound()
        {
            var customername = "falshiuka";

            Assert.Null(_customerRepository.GetUserWithUsername(customername));
        }

        [Fact]
        public void GetUserWithUserName_ShoudReturnUserIfSuchUsernameExists()
        {
            var customername = "existingUsername";
            var customer = new CustomerEntity { Username = customername };

            _customerRepository.Add(customer);
            _customerRepository.SaveChanges();

            Assert.Equal(customer, _customerRepository.GetUserWithUsername(customername));
        }

        [Fact]
        public void Remove_ShouldNotRemoveUserIfUsernameNotFound()
        {
            var customer = new CustomerEntity { Username = "fake" };
            var initialCount = (_customerRepository.GetAll().ToList()).Count;

            _customerRepository.Remove(customer);
            _customerRepository.SaveChanges();

            var allUsers = _customerRepository.GetAll().ToList();
            Assert.True(allUsers.Count == initialCount);
            Assert.DoesNotContain(customer, allUsers);
        }

        [Fact]
        public void Remove_ShouldRemoveSingleUser()
        {
            var customer = new CustomerEntity { Username = "delet this" };
            var initialCount = (_customerRepository.GetAll().ToList()).Count;

            _customerRepository.Add(customer);
            _customerRepository.SaveChanges();
            _customerRepository.Remove(customer);
            _customerRepository.SaveChanges();

            var allUsers = _customerRepository.GetAll().ToList();
            Assert.True(allUsers.Count == initialCount);
            Assert.DoesNotContain(customer, allUsers);
        }

        [Fact]
        public void RemoveAll_ShouldRemoveAllUsers()
        {
            _customerRepository.RemoveAll();
            _customerRepository.SaveChanges();

            Assert.Empty(_customerRepository.GetAll());
        }

        [Fact]
        public void RemoveRange_ShoudNotRemoveUserIfUsernameNotFound()
        {
            var customer1 = new CustomerEntity { Username = "remove1" };
            var customer2 = new CustomerEntity { Username = "remove2" };
            var initialCount = (_customerRepository.GetAll().ToList()).Count;

            _customerRepository.Add(customer1);
            _customerRepository.SaveChanges();
            _customerRepository.RemoveRange(new List<CustomerEntity> { customer1, customer2 });
            _customerRepository.SaveChanges();

            var allUsers = _customerRepository.GetAll().ToList();
            Assert.True(allUsers.Count == initialCount);
            Assert.DoesNotContain(customer1, allUsers);
            Assert.DoesNotContain(customer2, allUsers);
        }

        [Fact]
        public void RemoveRange_ShouldRemoveMultipleUsers()
        {
            var customer1 = new CustomerEntity { Username = "remove1" };
            var customer2 = new CustomerEntity { Username = "remove2" };

            _customerRepository.AddRange(new List<CustomerEntity> { customer1, customer2 });
            _customerRepository.SaveChanges();
            _customerRepository.RemoveRange(new List<CustomerEntity> { customer1, customer2 });
            _customerRepository.SaveChanges();

            var allUsers = _customerRepository.GetAll().ToList();
            Assert.DoesNotContain(customer1, allUsers);
            Assert.DoesNotContain(customer2, allUsers);
        }
    }
}