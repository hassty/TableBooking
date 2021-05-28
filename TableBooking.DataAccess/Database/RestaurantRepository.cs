﻿using Core.Contracts.DataAccess;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccess.Database
{
    public class RestaurantRepository : GenericRepository<RestaurantEntity>, IRestaurantRepository
    {
        private TableBookingContext _tableBookingContext => _context as TableBookingContext;

        public RestaurantRepository(DbContext context)
            : base(context)
        {
        }

        public RestaurantEntity GetRestaurantByNameAndAddress(string name, string address)
        {
            return _tableBookingContext.Restaurants
                .Include(r => r.OrderOptions)
                .Where(r => r.Name == name && r.Address == address)
                .FirstOrDefault();
        }
    }
}