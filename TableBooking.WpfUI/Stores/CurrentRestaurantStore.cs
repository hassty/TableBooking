using Core.Entities;
using System;

namespace WpfUI.Stores
{
    public class CurrentRestaurantStore
    {
        private RestaurantEntity _currentRestaurant;

        public RestaurantEntity CurrentRestaurant
        {
            get => _currentRestaurant;
            set
            {
                if (_currentRestaurant != value)
                {
                    _currentRestaurant = value;
                    CurrentRestaurantChanged?.Invoke();
                }
            }
        }

        public event Action CurrentRestaurantChanged;
    }
}