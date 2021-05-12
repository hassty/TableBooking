using AutoMapper;
using Core.Contracts;
using Core.Entities;
using Core.Entities.Users;
using Core.UseCases;
using DataAccess;
using DataAccess.Database;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Core.Tests
{
    public class CustomerServiceInteractorTests
    {
        private readonly CustomerServiceInteractor _customerServiceInteractor;
        private readonly IAdminRepository _adminRepository;
        private readonly ICustomerRespository _customerRespository;
        private readonly IMapper _mapper;

        public CustomerServiceInteractorTests()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<DataAccessMappingProfile>());
            _mapper = new Mapper(configuration);

            (_customerRespository, _adminRepository) = GetInMemoryRepositories();
            _customerServiceInteractor = new CustomerServiceInteractor(_customerRespository, _adminRepository);
        }

        private (ICustomerRespository, IAdminRepository) GetInMemoryRepositories()
        {
            var options = new DbContextOptionsBuilder<TableBookingContext>()
                .UseInMemoryDatabase(nameof(UserRepositoryTests))
                .Options;

            var context = new TableBookingContext(options);
            return (new CustomerRepository(context, _mapper), new AdminRepository(context, _mapper));
        }


        [Fact]
        public void AddOrder_ShouldAddOrderToCurrentCustomerAndAllAdmins()
        {
            var customer = new CustomerEntity { Username = "customer" };
            var restaurant = new RestaurantEntity { Name = "restaurant" };
            var order = new OrderEntity
            {
                Id = 228,
                Customer = customer,
                OrderDate = DateTime.Now,
                Restaurant = restaurant
            };

            _customerServiceInteractor.AddOrder(customer, order);

            Assert.True(customer.Orders.Count == 1);
        }
    }
}
