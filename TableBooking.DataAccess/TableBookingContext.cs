using Core.Entities;
using Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
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
            var dayOfWeekListConverter = new ValueConverter<IList<DayOfWeek>, string>(
                list => String.Join(',', list.Cast<int>()),
                str => str.Split(',', StringSplitOptions.RemoveEmptyEntries)
                      .Select(day => (DayOfWeek)Enum.Parse(typeof(DayOfWeek), day))
                      .ToList()
            );

            var user = modelBuilder.Entity<UserEntity>();
            var admin = modelBuilder.Entity<AdminEntity>();
            var customer = modelBuilder.Entity<CustomerEntity>();
            var restaurant = modelBuilder.Entity<RestaurantEntity>();
            var restaurantOrderOptions = modelBuilder.Entity<RestaurantOrderOptionsEntity>();

            user.HasAlternateKey(u => u.Username);
            user.Property(u => u.PasswordHash).HasMaxLength(44);

            admin.Ignore(a => a.UnconfirmedOrders);

            restaurant.HasAlternateKey(r => new { r.Name, r.Address });
            restaurantOrderOptions.Property(r => r.OffDays).HasConversion(dayOfWeekListConverter);

            base.OnModelCreating(modelBuilder);
        }
    }
}