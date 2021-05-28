using Core.Entities;
using Core.Exceptions;
using Core.UseCases;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
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
        private readonly AddRestaurant _addRestaurant;
        private readonly INavigationService _goBackNavigator;
        private readonly CurrentRestaurantStore _restaurantStore;
        private readonly UpdateRestaurant _updateRestaurant;
        private int _hoursFrom;
        private int _hoursTill;
        private int _minutesFrom;
        private int _minutesTill;
        private RestaurantEntity _restaurant;

        public ICommand AdditionalOptionsCommand { get; }
        public ICommand AddMenuItemsCommand { get; }
        public ICommand GoBackCommand { get; }
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
            AddRestaurant addRestaurant,
            UpdateRestaurant updateRestaurant,
            INavigationService goBackNavigator,
            INavigationService additionalOptionsNavigator,
            INavigationService addMenuItemsNavigator
        )
        {
            _restaurantStore = restaurantStore;
            _addRestaurant = addRestaurant;
            _updateRestaurant = updateRestaurant;
            _goBackNavigator = goBackNavigator;
            _additionalOptionsNavigator = additionalOptionsNavigator;
            _addMenuItemsNavigator = addMenuItemsNavigator;

            _restaurant = (restaurantStore.CurrentRestaurant != null)
                ? restaurantStore.CurrentRestaurant : new RestaurantEntity();

            AdditionalOptionsCommand = new DelegateCommand(NavigateToAdditionalOptions);
            AddMenuItemsCommand = new DelegateCommand(AddMenuItem);
            GoBackCommand = new DelegateCommand(_ => _goBackNavigator.Navigate());
            SaveCommand = new DelegateCommand(Save, CanAddRestaurant);
        }

        private void AddMenuItem(object obj)
        {
            _addMenuItemsNavigator.Navigate();
        }

        private bool CanAddRestaurant(object arg)
        {
            return !String.IsNullOrWhiteSpace(Name)
                && !String.IsNullOrWhiteSpace(City)
                && !String.IsNullOrWhiteSpace(Address);
        }

        private void NavigateToAdditionalOptions(object obj)
        {
            _additionalOptionsNavigator.Navigate();
        }

        private void Save(object obj)
        {
            if (_restaurantStore.CurrentRestaurant == null)
            {
                try
                {
                    _addRestaurant.Add(_restaurant);
                    _goBackNavigator.Navigate();
                }
                catch (ItemAlreadyExistsException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    _updateRestaurant.Update(_restaurant);
                    _goBackNavigator.Navigate();
                }
                catch (Exception)
                {
                    MessageBox.Show("Can not change name and address");
                }
            }
        }
    }
}