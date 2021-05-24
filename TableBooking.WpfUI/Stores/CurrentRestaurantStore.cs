using System;
using System.Collections.Generic;
using System.Text;
using WpfUI.Models;

namespace WpfUI.Stores
{
    public class CurrentRestaurantStore
    {
        private RestaurantModel _currentRestaurant;

        public RestaurantModel CurrentRestaurant
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