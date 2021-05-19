using Core.Contracts.DataAccess;
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
        private readonly ICustomerRepository _customerRespository;
        private readonly IOrderRepository _orderRepository;
        private readonly OrdersInteractor _ordersInteractor;
        private readonly IAdminRepository _adminRepository;

        public OrdersInteractorTests()
        {
            var databaseFixture = new DatabaseFixture(nameof(OrdersInteractorTests));

            _customerRespository = databaseFixture.CustomerRepository;
            _orderRepository = databaseFixture.OrderRepository;
            _adminRepository = databaseFixture.AdminRepository;

            _ordersInteractor = new OrdersInteractor(_orderRepository);
        }

        [Fact]
        public void AddOrder_ShouldAddOneUniqueOrder()
        {
            var customer = new CustomerEntity { Username = "unique order" };
            var admin = new AdminEntity { Username = "unique admin" };
            var order = new OrderEntity
            {
                Customer = customer,
            };

            _customerRespository.Add(customer);
            _customerRespository.SaveChanges();
            _adminRepository.Add(admin);
            _adminRepository.SaveChanges();
            _ordersInteractor.AddOrder(order);

            Assert.Contains(order, _orderRepository.GetAll());
            Assert.Contains(order, _orderRepository.GetAllOrdersOfCustomer(customer.Username));
            Assert.Contains(order, _orderRepository.GetAllUnconfirmedOrders());
        }
    }
}