using AutoMapper;
using Core.Entities;
using Core.Entities.Users;
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
        private readonly IMapper _mapper;
        private readonly CurrentUserStore _userStore;
        private readonly RestaurantInteractor _restaurantInteractor;
        private readonly AddOrder _addOrder;
        private readonly INavigationService _accountNavigationService;

        private OrderModel _order;
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

        private RestaurantModel _currentRestaurant { get; set; }
        public string Address => _currentRestaurant.Address;

        public List<int> Capacities { get; private set; }
        public string City => _currentRestaurant.City;
        public string Name => _currentRestaurant.Name;
        public ICommand AddOrderCommand { get; }

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

        private bool CanAddOrder(object parameter)
        {
            return true;
        }

        private void AddOrder(object parameter)
        {
            try
            {
                if (_userStore.CurrentUser is CustomerModel customer)
                {
                    var orderEntity = new OrderEntity(_order.ReservationDate, _order.ReservationDuration);

                    _addOrder.Add(
                        orderEntity,
                        _userStore.CurrentUser.Username,
                        _currentRestaurant.Name,
                        _currentRestaurant.Address);
                    _accountNavigationService.Navigate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetCapacities()
        {
            Capacities = _restaurantInteractor.GetRestaurantTablesCapacities(_currentRestaurant.Name, _currentRestaurant.Address);
        }
    }
}