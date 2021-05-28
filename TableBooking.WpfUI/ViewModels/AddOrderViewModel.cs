using AutoMapper;
using Core.Entities;
using Core.UseCases;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using WpfUI.Commands;
using WpfUI.Models;
using WpfUI.Services;
using WpfUI.Stores;

namespace WpfUI.ViewModels
{
    public class AddOrderViewModel : ViewModelBase
    {
        private readonly INavigationService _accountNavigationService;
        private readonly AddOrder _addOrder;
        private readonly IMapper _mapper;
        private readonly RestaurantInteractor _restaurantInteractor;
        private readonly CurrentUserStore _userStore;
        private int _hours;
        private int _minutes;
        private OrderModel _order;
        private RestaurantModel _currentRestaurant { get; set; }
        public ICommand AddOrderCommand { get; }
        public string Address => _currentRestaurant.Address;
        public List<int> Capacities { get; private set; }
        public string City => _currentRestaurant.City;

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

        public List<MenuItemModel> MenuItems { get; set; }

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

        public TimeSpan ReservationDuration
        {
            get => _order.ReservationDuration;
            set
            {
                if (_order.ReservationDuration != value)
                {
                    _order.ReservationDuration = value;
                    OnPropertyChanged(nameof(ReservationDuration));
                }
            }
        }

        public AddOrderViewModel(
            CurrentRestaurantStore restaurantStore,
            CurrentUserStore userStore,
            RestaurantInteractor restaurantInteractor,
            AddOrder addOrder,
            INavigationService accountNavigationService,
            IMapper mapper
        )
        {
            _currentRestaurant = restaurantStore.CurrentRestaurant;
            _userStore = userStore;
            _restaurantInteractor = restaurantInteractor;
            _addOrder = addOrder;
            _accountNavigationService = accountNavigationService;
            _mapper = mapper;

            _order = new OrderModel();
            GetCapacities();

            AddOrderCommand = new DelegateCommand(AddOrder, CanAddOrder);
        }

        private void AddOrder(object parameter)
        {
            try
            {
                if (_userStore.CurrentUser is CustomerModel customer)
                {
                    var orderEntity = new OrderEntity(
                        _mapper.Map<RestaurantEntity>(_currentRestaurant),
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

        public void GetCapacities()
        {
            Capacities = _restaurantInteractor.GetRestaurantTablesCapacities(_currentRestaurant.Name, _currentRestaurant.Address);
        }
    }
}