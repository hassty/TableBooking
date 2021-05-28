using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WpfUI.Commands;
using WpfUI.Services;
using WpfUI.Stores;

namespace WpfUI.ViewModels
{
    public class RestaurantDetailsViewModel : ViewModelBase
    {
        private readonly INavigationService _additionalOptionsNavigator;
        private readonly INavigationService _addMenuItemsNavigator;
        private readonly CurrentRestaurantStore _restaurantStore;
        private int _hoursFrom;
        private int _hoursTill;
        private int _minutesFrom;
        private int _minutesTill;
        private RestaurantEntity _restaurant;
        public ICommand AdditionalOptionsCommand { get; }
        public ICommand AddMenuItemsCommand { get; }
        public ICommand SaveCommand { get; }

        #region Bindable Properties

        public string Address
        {
            get => _restaurant.Address;
            set
            {
                if (_restaurant.Address != value)
                {
                    _restaurant.Address = value;
                    OnPropertyChanged(nameof(Address));
                }
            }
        }

        public string City
        {
            get => _restaurant.City;
            set
            {
                if (_restaurant.City != value)
                {
                    _restaurant.City = value;
                    OnPropertyChanged(nameof(City));
                }
            }
        }

        public int HoursFrom
        {
            get => _hoursFrom;
            set
            {
                if (_hoursFrom != value)
                {
                    _hoursFrom = value;
                    OnPropertyChanged(nameof(HoursFrom));
                }
            }
        }

        public int HoursTill
        {
            get => _hoursTill;
            set
            {
                if (_hoursTill != value)
                {
                    _hoursTill = value;
                    OnPropertyChanged(nameof(HoursTill));
                }
            }
        }

        public int MinutesFrom
        {
            get => _minutesFrom;
            set
            {
                if (_minutesFrom != value)
                {
                    _minutesFrom = value;
                    OnPropertyChanged(nameof(MinutesFrom));
                }
            }
        }

        public int MinutesTill
        {
            get => _minutesTill;
            set
            {
                if (_minutesTill != value)
                {
                    _minutesTill = value;
                    OnPropertyChanged(nameof(MinutesTill));
                }
            }
        }

        public string Name
        {
            get => _restaurant.Name;
            set
            {
                if (_restaurant.Name != value)
                {
                    _restaurant.Name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        #endregion Bindable Properties

        public RestaurantDetailsViewModel(
            CurrentRestaurantStore restaurantStore,
            INavigationService additionalOptionsNavigator,
            INavigationService addMenuItemsNavigator
        )
        {
            _restaurantStore = restaurantStore;
            _additionalOptionsNavigator = additionalOptionsNavigator;
            _addMenuItemsNavigator = addMenuItemsNavigator;
            _restaurant = new RestaurantEntity();

            AdditionalOptionsCommand = new DelegateCommand(NavigateToAdditionalOptions);
            AddMenuItemsCommand = new DelegateCommand(AddMenuItem);
        }

        private void AddMenuItem(object obj)
        {
            _addMenuItemsNavigator.Navigate();
        }

        private void NavigateToAdditionalOptions(object obj)
        {
            _additionalOptionsNavigator.Navigate();
        }
    }
}