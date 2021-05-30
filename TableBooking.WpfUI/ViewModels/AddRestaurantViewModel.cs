using Core.Entities;
using Core.Exceptions;
using Core.UseCases;
using System;
using System.Windows;
using System.Windows.Input;
using WpfUI.Commands;
using WpfUI.Services;

namespace WpfUI.ViewModels
{
    public class AddRestaurantViewModel : ViewModelBase
    {
        private readonly AddRestaurant _addRestaurant;
        private readonly INavigationService _goBackNavigator;
        private RestaurantEntity _restaurant;

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

        public AddRestaurantViewModel(
            AddRestaurant addRestaurant,
            INavigationService goBackNavigator
        )
        {
            _addRestaurant = addRestaurant;
            _goBackNavigator = goBackNavigator;

            _restaurant = new RestaurantEntity();

            AddMenuItemsCommand = new DelegateCommand(AddMenuItem);
            GoBackCommand = new DelegateCommand(_ => goBackNavigator.Navigate());
            SaveCommand = new DelegateCommand(Save, CanAddRestaurant);
        }

        private void AddMenuItem(object obj)
        {
        }

        private bool CanAddRestaurant(object arg)
        {
            return !String.IsNullOrWhiteSpace(Name)
                && !String.IsNullOrWhiteSpace(City)
                && !String.IsNullOrWhiteSpace(Address);
        }

        private void Save(object obj)
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
    }
}