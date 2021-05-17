using AutoMapper;
using Core.Contracts;
using Core.Entities;
using Core.Entities.Menu;
using Core.Entities.Users;
using Core.UseCases;
using DataAccess;
using DataAccess.Database;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Core.Tests
{
    public class CustomerServiceInteractorTests
    {
        private readonly IAdminRepository _adminRepository;
        private readonly ICustomerRespository _customerRespository;
        private readonly CustomerServiceInteractor _customerServiceInteractor;
        private readonly IMapper _mapper;

        public CustomerServiceInteractorTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DataAccessMappingProfile>();
            });
            configuration.AssertConfigurationIsValid();
            _mapper = new Mapper(configuration);

            (_customerRespository, _adminRepository) = GetInMemoryRepositories();
            _customerServiceInteractor = new CustomerServiceInteractor(_customerRespository, _adminRepository);
        }

        private (ICustomerRespository, IAdminRepository) GetInMemoryRepositories()
        {
            var options = new DbContextOptionsBuilder<TableBookingContext>()
                .UseInMemoryDatabase(nameof(CustomerServiceInteractorTests))
                .EnableSensitiveDataLogging()
                .Options;

            var context = new TableBookingContext(options);
            return (new CustomerRepository(context, _mapper), new AdminRepository(context, _mapper));
        }

        [Fact]
        public void AddOrder_ShouldAddMultipleOrdersToSameCustomerAndAllAdmins()
        {
            var customer = new CustomerEntity { Username = "multiple customer" };
            var restaurant = new RestaurantEntity { Name = "multiple restaurant" };
            var admin1 = new AdminEntity { Username = "multiple admin 1" };
            var admin2 = new AdminEntity { Username = "multiple admin 2" };
            var table = new TableEntity { Id = 2, Capacity = 3, Restaurant = restaurant };
            var menuItems = new List<MenuItemEntity>{
                new MenuItemEntity { Name = "cocktail" }
            };
            var order1 = new OrderEntity
            {
                Id = 1337,
                Customer = customer,
                Restaurant = restaurant,
                Table = table,
                MenuItems = menuItems
            };
            var order2 = new OrderEntity
            {
                Id = 1488,
                Customer = customer,
                Restaurant = restaurant,
                Table = table,
                MenuItems = menuItems
            };

            _adminRepository.AddRange(new List<AdminEntity> { admin1, admin2 });
            _adminRepository.SaveChanges();
            _customerServiceInteractor.AddOrder(customer, order1);
            _customerServiceInteractor.AddOrder(customer, order2);

            var admins = _adminRepository.GetAllAdmins().ToList();
            Assert.True(customer.Orders.Count == 1);
            Assert.Contains(order1, customer.Orders);
            Assert.Contains(order2, customer.Orders);
            foreach (var dbAdmin in admins)
            {
                Assert.Contains(order1, dbAdmin.UnconfirmedOrders);
                Assert.Contains(order2, dbAdmin.UnconfirmedOrders);
            }
        }

        [Fact]
        public void AddOrder_ShouldAddOneOrderToCurrentCustomerAndAllAdmins()
        {
            var customer = new CustomerEntity { Username = "customer" };
            var restaurant = new RestaurantEntity { Name = "restaurant" };
            var admin1 = new AdminEntity { Username = "admin1" };
            var admin2 = new AdminEntity { Username = "admin2" };
            var table = new TableEntity { Id = 1, Capacity = 2, Restaurant = restaurant };
            var menuItems = new List<MenuItemEntity>{
                new MenuItemEntity { Name = "cocka-cola" }
            };
            var order = new OrderEntity
            {
                Id = 228,
                Customer = customer,
                Restaurant = restaurant,
                Table = table,
                MenuItems = menuItems
            };

            _adminRepository.AddRange(new List<AdminEntity> { admin1, admin2 });
            _adminRepository.SaveChanges();
            _customerServiceInteractor.AddOrder(customer, order);

            var admins = _adminRepository.GetAllAdmins().ToList();
            Assert.True(customer.Orders.Count == 1);
            Assert.Contains(order, customer.Orders);
            foreach (var dbAdmin in admins)
            {
                Assert.Contains(order, dbAdmin.UnconfirmedOrders);
            }
        }

        [Fact]
        public void AddOrder_ShouldAddOneOrderToCurrentCustomerAndOneAdmin()
        {
            var customer = new CustomerEntity { Username = "customer" };
            var restaurant = new RestaurantEntity { Name = "restaurant" };
            var admin = new AdminEntity { Username = "single admin" };
            var table = new TableEntity { Id = 3, Capacity = 2, Restaurant = restaurant };
            var menuItems = new List<MenuItemEntity>{
                new MenuItemEntity { Name = "cocka-cola" }
            };
            var order = new OrderEntity
            {
                Id = 42,
                Customer = customer,
                Restaurant = restaurant,
                Table = table,
                MenuItems = menuItems
            };

            _adminRepository.RemoveAll();
            _adminRepository.Add(admin);
            _adminRepository.SaveChanges();
            _customerServiceInteractor.AddOrder(customer, order);

            var admins = _adminRepository.GetAllAdmins().ToList();
            Assert.True(customer.Orders.Count == 1);
            Assert.Contains(order, customer.Orders);
            foreach (var dbAdmin in admins)
            {
                Assert.Contains(order, dbAdmin.UnconfirmedOrders);
            }
        }
    }
}