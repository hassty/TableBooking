using Core.Entities;
using Core.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Entities
{
    public class TableBookingContext : DbContext
    {
        public DbSet<AdminEntity> Admins { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }

        public DbSet<RestaurantEntity> Restaurants { get; set; }

        public DbSet<UserEntity> Users { get; set; }

        public TableBookingContext(DbContextOptions<TableBookingContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var user = modelBuilder.Entity<UserEntity>();
            var admin = modelBuilder.Entity<AdminEntity>();

            user.HasAlternateKey(u => u.Username);
            user.Property(u => u.PasswordHash).HasMaxLength(44);

            admin.Ignore(a => a.UnconfirmedOrders);

            base.OnModelCreating(modelBuilder);
        }
    }
}