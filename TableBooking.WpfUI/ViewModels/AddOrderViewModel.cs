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
        private readonly GetRestaurants _getRestaurants;
        private readonly OrderEntity _order;
        private readonly CurrentUserStore _userStore;
        private int _hours;
        private int _minutes;
        private RestaurantEntity _currentRestaurant { get; set; }
        public ICommand AddOrderCommand { get; }
        public string Address => _currentRestaurant.Address;

        public int Hours
        {
            get => _hours;
            set
            {
                if (_hours != value)
                {
                    _hours = value;
                    OnPropertyChanged(nameof(Hours));
                }
            }
        }

        public List<MenuItemEntity> MenuItems { get; set; }

        public int Minutes
        {
            get => _minutes;
            set
            {
                if (_minutes != value)
                {
                    _minutes = value;
                    OnPropertyChanged(nameof(Minutes));
                }
            }
        }

        public string Name => _currentRestaurant.Name;
        public List<int> PartySizes { get; private set; }

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

        public AddOrderViewModel(
            CurrentRestaurantStore restaurantStore,
            CurrentUserStore userStore,
            AddOrder addOrder,
            GetRestaurants getRestaurants,
            INavigationService accountNavigationService
        )
        {
            _currentRestaurant = restaurantStore.CurrentRestaurant;
            _userStore = userStore;
            _addOrder = addOrder;
            _getRestaurants = getRestaurants;
            _accountNavigationService = accountNavigationService;

            _order = new OrderEntity();
            GetRestaurantPartySizes();

            AddOrderCommand = new DelegateCommand(AddOrder, CanAddOrder);
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
                        new TimeSpan(_hours, _minutes, 0)
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

        private bool CanAddOrder(object parameter)
        {
            return true;
        }

        public void GetRestaurantPartySizes()
        {
            PartySizes = _getRestaurants.GetRestaurantByNameAndAddress(Name, Address).GetPartySizes().ToList();
        }
    }
}