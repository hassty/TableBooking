using Core.Contracts.DataAccess;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.UseCases
{
    public class AddMenuItem
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public AddMenuItem(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public void Add(RestaurantEntity restaurant, MenuItemEntity menuItem)
        {
            _restaurantRepository.Get(restaurant.Id).MenuItems.Add(menuItem);
            _restaurantRepository.SaveChanges();
        }
    }
}