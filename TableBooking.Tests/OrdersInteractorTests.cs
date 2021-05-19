﻿using Core.Contracts;
using Core.Entities;
using Core.Entities.Users;
using Core.UseCases;
using DataAccess.Database;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Core.Tests
{
    public class OrdersInteractorTests
    {
        private readonly ICustomerRespository _customerRespository;
        private readonly IOrderRepository _orderRepository;
        private readonly OrdersInteractor _ordersInteractor;

        public OrdersInteractorTests()
        {
            (_orderRepository, _customerRespository) = GetInMemoryRepositories();
            _ordersInteractor = new OrdersInteractor(_orderRepository);
        }

        private (IOrderRepository, ICustomerRespository) GetInMemoryRepositories()
        {
            var options = new DbContextOptionsBuilder<TableBookingContext>()
                .UseInMemoryDatabase(nameof(OrdersInteractorTests))
                .EnableSensitiveDataLogging()
                .Options;

            var context = new TableBookingContext(options);
            return (new OrderRepository(context), new CustomerRepository(context));
        }

        [Fact]
        public void AddOrder_ShouldAddOneUniqueOrder()
        {
            var customer = new CustomerEntity { Username = "unique order" };
            var order = new OrderEntity
            {
                Customer = customer,
            };

            _customerRespository.Add(customer);
            _customerRespository.SaveChanges();
            _ordersInteractor.AddOrder(order);

            Assert.Contains(order, _orderRepository.GetAll());
            Assert.Contains(order, _orderRepository.GetAllOrdersOfCustomer(customer.Username));
        }
    }
}