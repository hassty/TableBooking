using Core.Contracts.DataAccess;
using Core.Entities;
using Core.Entities.Users;
using Core.UseCases;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Core.Tests
{
    public class AddOrderTests
    {
        private readonly AddOrder _addOrder;
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IRestaurantRepository _restaurantRepository;

        public AddOrderTests()
        {
            var databaseFixture = new DatabaseFixture(nameof(AddOrderTests));

            _orderRepository = databaseFixture.OrderRepository;
            _customerRepository = databaseFixture.CustomerRepository;
            _restaurantRepository = databaseFixture.RestaurantRepository;
            _addOrder = new AddOrder(_orderRepository, _customerRepository, _restaurantRepository);
        }

        [Fact]
        public void Add_ShouldAddNewOrderToCustomerAndRestaurant()
        {
            var customer = new CustomerEntity() { Username = "add order customer" };
            _customerRepository.Add(customer);
            _customerRepository.SaveChanges();
            var restaurant = new RestaurantEntity() { Name = "makdak", Address = "addr" };
            _restaurantRepository.Add(restaurant);
            _restaurantRepository.SaveChanges();
            var order = new OrderEntity(DateTime.Now.AddDays(1), new TimeSpan(2, 0, 0));

            _addOrder.Add(order, customer.Username, restaurant.Name, restaurant.Address);

            Assert.Contains(order, _orderRepository.GetAllOrdersOfCustomer(customer.Username));
        }

        [Fact]
        public void Add_ShouldFailAddingOrderForPastDate()
        {
            var customer = new CustomerEntity() { Username = "add order in the past" };
            _customerRepository.Add(customer);
            _customerRepository.SaveChanges();
            var restaurant = new RestaurantEntity() { Name = "past makdak", Address = "addr" };
            _restaurantRepository.Add(restaurant);
            _restaurantRepository.SaveChanges();
            var order = new OrderEntity(DateTime.Now.AddDays(-1), new TimeSpan(2, 0, 0));

            _addOrder.Add(order, customer.Username, restaurant.Name, restaurant.Address);

            Assert.DoesNotContain(order, _orderRepository.GetAllOrdersOfCustomer(customer.Username));
        }

        [Fact]
        public void Add_ShouldFailAddingOrderForDayOff()
        {
            var dayOff = DateTime.Now.AddDays(2);
            var customer = new CustomerEntity() { Username = "add order in the past" };
            _customerRepository.Add(customer);
            _customerRepository.SaveChanges();
            var restaurant = new RestaurantEntity()
            {
                Name = "dayOff makdak",
                Address = "addr",
                OffDays = new List<DayOfWeek> { dayOff.DayOfWeek }
            };
            _restaurantRepository.Add(restaurant);
            _restaurantRepository.SaveChanges();
            var order = new OrderEntity(dayOff, new TimeSpan(2, 0, 0));

            _addOrder.Add(order, customer.Username, restaurant.Name, restaurant.Address);

            Assert.DoesNotContain(order, _orderRepository.GetAllOrdersOfCustomer(customer.Username));
        }

    }
}
