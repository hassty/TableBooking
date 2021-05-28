using Core.Contracts.DataAccess;
using DataAccess;
using DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using System;

namespace Core.Tests
{
    public class DatabaseFixture : IDisposable
    {
        public IAdminRepository AdminRepository { get; private set; }
        public DbContext Context { get; private set; }
        public ICustomerRepository CustomerRepository { get; private set; }
        public IOrderRepository OrderRepository { get; private set; }
        public IRestaurantRepository RestaurantRepository { get; private set; }

        public DatabaseFixture(string databaseName)
        {
            var options = new DbContextOptionsBuilder<TableBookingContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

            Context = new TableBookingContext(options);

            CustomerRepository = new CustomerRepository(Context);
            OrderRepository = new OrderRepository(Context);
            AdminRepository = new AdminRepository(Context);
            RestaurantRepository = new RestaurantRepository(Context);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}