using Core.Dto;
using Core.Dto.Users;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess.Entities
{
    public class TableBookingContext : DbContext
    {
        public DbSet<RestaurantDto> Restaurants { get; set; }
        public DbSet<UserDto> Users { get; set; }

        public TableBookingContext(DbContextOptions<TableBookingContext> options)
            : base(options)
        {
            Database.EnsureCreated();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var user = modelBuilder.Entity<UserDto>();
            user.HasAlternateKey(u => u.Username);
            user.Property(u => u.PasswordHash).HasMaxLength(44);

            var restaurant = modelBuilder.Entity<RestaurantDto>()
                .HasData(new[] {
                    new RestaurantDto
                    {
                        Id = 1,
                        Name = "makdak",
                        Address = "ulica pushkina, dom kukushkina",
                        City = "minsk",
                        OpenedFrom = new TimeSpan(7, 0, 0),
                        OpenedTill = new TimeSpan(22, 30, 0)
                    },
                    new RestaurantDto
                    {
                        Id = 2,
                        Name = "buerkinh",
                        Address = "kosmonavtov 54/1",
                        City = "minsk",
                        OpenedFrom = new TimeSpan(9, 0, 0),
                        OpenedTill = new TimeSpan(21, 0, 0)
                    }
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}