using Core.Entities;
using Core.Exceptions;
using Core.UseCases;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using WpfUI.Commands;
using WpfUI.Services;
using WpfUI.Stores;

namespace WpfUI.ViewModels
{
    public class UnconfirmedOrdersViewModel : ViewModelBase
    {
        private readonly ConfirmOrder _confirmOrder;
        private readonly GetAllUnconfirmedOrders _getAllUnconfirmedOrders;
        private readonly INavigationService _homeNavigationService;
        private readonly INavigationService _restaurantNavigationService;
        private readonly CurrentUserStore _userStore;
        private OrderEntity _selectedOrder;

        public ICommand ConfirmOrderCommand { get; }
        public ICommand GoToRestaurantsCommand { get; }
        public ICommand LogoutCommand { get; }

        public OrderEntity SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                if (_selectedOrder != value)
                {
                    _selectedOrder = value;
                    OnPropertyChanged(nameof(SelectedOrder));
                }
            }
        }

        public List<OrderEntity> UnconfirmedOrders { get; set; }

        public UnconfirmedOrdersViewModel(
            CurrentUserStore userStore,
            GetAllUnconfirmedOrders getAllUnconfirmedOrders,
            ConfirmOrder confirmOrder,
            INavigationService restaurantNavigationService,
            INavigationService homeNavigationService
        )
        {
            _userStore = userStore;
            _getAllUnconfirmedOrders = getAllUnconfirmedOrders;
            _confirmOrder = confirmOrder;
            _restaurantNavigationService = restaurantNavigationService;
            _homeNavigationService = homeNavigationService;

            ConfirmOrderCommand = new DelegateCommand(Confirm, CanConfirm);
            GoToRestaurantsCommand = new DelegateCommand(_ => _restaurantNavigationService.Navigate());
            LogoutCommand = new DelegateCommand(Logout);

            LoadUnconfirmedOrders();
        }

        private bool CanConfirm(object parameter)
        {
            return _selectedOrder != null && UnconfirmedOrders.Count != 0;
        }

        private void Confirm(object parameter)
        {
            try
            {
                _confirmOrder.Confirm(_selectedOrder);
            }
            catch (NotifierException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadUnconfirmedOrders();
            }
        }

        private void LoadUnconfirmedOrders()
        {
            UnconfirmedOrders = new List<OrderEntity>(_getAllUnconfirmedOrders.GetOrders());
            OnPropertyChanged(nameof(UnconfirmedOrders));
        }

        private void Logout(object obj)
        {
            _userStore.Logout();
            _homeNavigationService.Navigate();
        }
    }
}