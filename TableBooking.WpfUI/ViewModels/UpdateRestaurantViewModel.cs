using Core.Entities;
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
    public class UpdateRestaurantViewModel : ViewModelBase
    {
        private readonly CancelRestaurantChanges _cancelRestaurantChanges;
        private readonly INavigationService _goBackNavigator;
        private readonly CurrentRestaurantStore _restaurantStore;
        private readonly string _unchangedAddress;
        private readonly string _unchangedName;
        private readonly UpdateRestaurant _updateRestaurant;
        private RestaurantEntity _restaurant;

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
            get => _restaurant.OpenedFrom.Hours;
            set
            {
                if (_restaurant.OpenedFrom.Hours != value)
                {
                    _restaurant.OpenedFrom = new TimeSpan(value, MinutesFrom, 0);
                    OnPropertyChanged(nameof(HoursFrom));
                }
            }
        }

        public int HoursTill
        {
            get => _restaurant.OpenedTill.Hours;
            set
            {
                if (_restaurant.OpenedTill.Hours != value)
                {
                    _restaurant.OpenedTill = new TimeSpan(value, MinutesTill, 0);
                    OnPropertyChanged(nameof(HoursTill));
                }
            }
        }

        public int MinutesFrom
        {
            get => _restaurant.OpenedFrom.Minutes;
            set
            {
                if (_restaurant.OpenedFrom.Minutes != value)
                {
                    _restaurant.OpenedFrom = new TimeSpan(HoursFrom, value, 0);
                    OnPropertyChanged(nameof(MinutesFrom));
                }
            }
        }

        public int MinutesTill
        {
            get => _restaurant.OpenedTill.Minutes;
            set
            {
                if (_restaurant.OpenedTill.Minutes != value)
                {
                    _restaurant.OpenedTill = new TimeSpan(HoursTill, value, 0);
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

        public UpdateRestaurantViewModel(
            CurrentRestaurantStore restaurantStore,
            UpdateRestaurant updateRestaurant,
            CancelRestaurantChanges cancelRestaurantChanges,
            INavigationService goBackNavigator
        )
        {
            _restaurantStore = restaurantStore;
            _cancelRestaurantChanges = cancelRestaurantChanges;
            _updateRestaurant = updateRestaurant;
            _goBackNavigator = goBackNavigator;

            _restaurant = restaurantStore.CurrentRestaurant;
            _unchangedName = _restaurant.Name;
            _unchangedAddress = _restaurant.Address;

            GoBackCommand = new DelegateCommand(Cancel);
            SaveCommand = new DelegateCommand(Save, CanAddRestaurant);
        }

        private bool CanAddRestaurant(object arg)
        {
            return !String.IsNullOrWhiteSpace(Name)
                && !String.IsNullOrWhiteSpace(City)
                && !String.IsNullOrWhiteSpace(Address);
        }

        private void Cancel(object obj)
        {
            _restaurantStore.CurrentRestaurant.Name = _unchangedName;
            _restaurantStore.CurrentRestaurant.Address = _unchangedAddress;
            _cancelRestaurantChanges.Cancel();

            _goBackNavigator.Navigate();
        }

        private void Save(object obj)
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