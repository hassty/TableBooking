using Core.Entities;
using Core.Entities.Users;
using Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WpfUI.Commands;
using WpfUI.Services;
using WpfUI.Stores;

namespace WpfUI.ViewModels
{
    public class AddOrderViewModel : ViewModelBase
    {
        private readonly INavigationService _accountNavigationService;
        private readonly AddOrder _addOrder;
        private readonly GetRestaurantMenuItems _getRestaurantMenuItems;
        private readonly GetRestaurants _getRestaurants;
        private readonly OrderEntity _order;
        private readonly CurrentUserStore _userStore;
        private MenuItemEntity _selectedMenuItem;
        private RestaurantEntity _currentRestaurant { get; set; }

        public ICommand AddItemCommand { get; }
        public ICommand AddOrderCommand { get; }
        public string Address => _currentRestaurant.Address;
        public ICommand GoBackCommand { get; }

        public int HoursDuration
        {
            get => _order.ReservationDuration.Hours;
            set
            {
                if (_order.ReservationDuration.Hours != value)
                {
                    _order.ReservationDuration = new TimeSpan(value, MinutesDuration, 0);
                    OnPropertyChanged(nameof(HoursDuration));
                }
            }
        }

        public List<MenuItemEntity> MenuItems { get; set; }

        public int MinutesDuration
        {
            get => _order.ReservationDuration.Minutes;
            set
            {
                if (_order.ReservationDuration.Minutes != value)
                {
                    _order.ReservationDuration = new TimeSpan(HoursDuration, value, 0);
                    OnPropertyChanged(nameof(MinutesDuration));
                }
            }
        }

        public string Name => _currentRestaurant.Name;

        public List<int> PartySizes { get; private set; }

        public ICommand RemoveItemCommand { get; }

        public DateTime ReservationDate
        {
            get => _order.ReservationDate;
            set
            {
                if (_order.ReservationDate != value)
                {
                    _order.ReservationDate = value;
                    OnPropertyChanged(nameof(ReservationDate));
                }
            }
        }

        public int ReservationHours
        {
            get => _order.ReservationDate.Hour;
            set
            {
                if (_order.ReservationDate.Hour != value)
                {
                    _order.ReservationDate = _order.ReservationDate.Date + new TimeSpan(value, ReservationMinutes, 0);
                    OnPropertyChanged(nameof(ReservationHours));
                }
            }
        }

        public int ReservationMinutes
        {
            get => _order.ReservationDate.Minute;
            set
            {
                if (_order.ReservationDate.Minute != value)
                {
                    _order.ReservationDate = _order.ReservationDate.Date + new TimeSpan(ReservationHours, value, 0);
                    OnPropertyChanged(nameof(ReservationMinutes));
                }
            }
        }

        public MenuItemEntity SelectedMenuItem
        {
            get => _selectedMenuItem;
            set
            {
                if (_selectedMenuItem != value)
                {
                    _selectedMenuItem = value;
                    OnPropertyChanged(nameof(SelectedMenuItem));
                }
            }
        }

        public decimal TotalPrice => _order.TotalPrice;

        public AddOrderViewModel(
            CurrentRestaurantStore restaurantStore,
            CurrentUserStore userStore,
            AddOrder addOrder,
            GetRestaurants getRestaurants,
            GetRestaurantMenuItems getRestaurantMenuItems,
            INavigationService accountNavigationService,
            INavigationService goBackNavigationService
        )
        {
            _currentRestaurant = restaurantStore.CurrentRestaurant;
            _userStore = userStore;
            _addOrder = addOrder;
            _getRestaurants = getRestaurants;
            _getRestaurantMenuItems = getRestaurantMenuItems;
            _accountNavigationService = accountNavigationService;

            _order = new OrderEntity();

            GetRestaurantPartySizes();
            LoadMenuItems();

            GoBackCommand = new DelegateCommand(_ => goBackNavigationService.Navigate());
            AddItemCommand = new DelegateCommand(AddItem, CanAddItem);
            RemoveItemCommand = new DelegateCommand(RemoveItem, CanRemoveItem);
            AddOrderCommand = new DelegateCommand(AddOrder);
        }

        private void AddItem(object obj)
        {
            _order.MenuItems.Add(_selectedMenuItem);
            OnPropertyChanged(nameof(TotalPrice));
        }

        private void AddOrder(object parameter)
        {
            try
            {
                if (_userStore.CurrentUser is CustomerEntity customer)
                {
                    var orderEntity = new OrderEntity(
                        _currentRestaurant,
                        _order.ReservationDate,
                        _order.ReservationDuration
                    );

                    _addOrder.Add(orderEntity, _userStore.CurrentUser.Username);
                    _accountNavigationService.Navigate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool CanAddItem(object arg)
        {
            return _selectedMenuItem != null;
        }

        private bool CanRemoveItem(object arg)
        {
            return _order.MenuItems != null && _order.MenuItems.Count > 0;
        }

        private void LoadMenuItems()
        {
            MenuItems = _getRestaurantMenuItems.GetMenuItems(_currentRestaurant);
            OnPropertyChanged(nameof(MenuItems));
        }

        private void RemoveItem(object obj)
        {
            _order.MenuItems.Remove(_selectedMenuItem);
            OnPropertyChanged(nameof(TotalPrice));
        }

        public void GetRestaurantPartySizes()
        {
            PartySizes = _getRestaurants.GetRestaurantByNameAndAddress(Name, Address).GetPartySizes().ToList();
        }
    }
}