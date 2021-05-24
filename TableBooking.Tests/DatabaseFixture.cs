using Core.Contracts.DataAccess;
using DataAccess.Database;
using DataAccess.Entities;
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

        public DatabaseFixture(string databaseName)
        {
            var options = new DbContextOptionsBuilder<TableBookingContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

            Context = new TableBookingContext(options);

            CustomerRepository = new CustomerRepository(Context);
            OrderRepository = new OrderRepository(Context);
            AdminRepository = new AdminRepository(Context);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}