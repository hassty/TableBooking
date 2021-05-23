using Core.Entities;
using Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess.Entities
{
    public class TableBookingContext : DbContext
    {
        public DbSet<AdminEntity> Admins { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }

        public DbSet<RestaurantEntity> Restaurants { get; set; }

        public TableBookingContext(DbContextOptions<TableBookingContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var user = modelBuilder.Entity<UserEntity>();
            var admin = modelBuilder.Entity<AdminEntity>();
            var customer = modelBuilder.Entity<CustomerEntity>();
            var restaurant = modelBuilder.Entity<RestaurantEntity>();

            user.HasAlternateKey(u => u.Username);
            user.Property(u => u.PasswordHash).HasMaxLength(44);

            admin.Ignore(a => a.UnconfirmedOrders);

            restaurant.HasData(new[]
            {
                new RestaurantEntity()
                {
                    Id=1,
                    Address = "addr1",
                    City="city1",
                    Name = "makdak",
                    OpenedFrom=new TimeSpan(4,20,0),
                    OpenedTill = new TimeSpan(23,0,0),
                },
                new RestaurantEntity()
                {
                    Id=2,
                    Address = "addr2",
                    City="city2",
                    Name = "burer kinh",
                    OpenedFrom=new TimeSpan(2,28,0),
                    OpenedTill = new TimeSpan(22,0,0),
                },
                new RestaurantEntity()
                {
                    Id=3,
                    Address = "addr3",
                    City="city1",
                    Name = "picca",
                    OpenedFrom=new TimeSpan(2,15,0),
                    OpenedTill = new TimeSpan(22,30,0),
                }
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}