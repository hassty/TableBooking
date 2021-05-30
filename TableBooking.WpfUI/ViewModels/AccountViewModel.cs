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
    public class AccountViewModel : ViewModelBase
    {
        private readonly CurrentUserStore _accountStore;
        private readonly CancelOrder _cancelOrder;
        private readonly GetCustomerOrders _getOrders;
        private readonly INavigationService _orderReportNavigator;
        private readonly CurrentRestaurantStore _restaurantStore;
        private OrderEntity _selectedOrder;
        public ICommand CancelSelectedOrderCommand { get; }
        public ICommand ConfirmOrderCommand { get; }
        public bool IsCustomer => _accountStore.IsCustomer;
        public ICommand NavigateHomeCommand { get; }
        public List<OrderEntity> Orders { get; set; }
        public ICommand SeeMenuItemsCommand { get; }

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

        public string Username => _accountStore.CurrentUser?.Username;

        public AccountViewModel(
            CurrentUserStore accountStore,
            CurrentRestaurantStore restaurantStore,
            INavigationService homeNavigationService,
            INavigationService orderReportNavigator,
            GetCustomerOrders getOrders,
            CancelOrder cancelOrder
        )
        {
            _accountStore = accountStore;
            _restaurantStore = restaurantStore;
            _orderReportNavigator = orderReportNavigator;
            _getOrders = getOrders;
            _cancelOrder = cancelOrder;

            _accountStore.CurrentAccountChanged += OnCurrentAccountChanged;

            NavigateHomeCommand = new NavigateCommand(homeNavigationService);
            CancelSelectedOrderCommand = new DelegateCommand(CancelOrder, OrderSelected);
            SeeMenuItemsCommand = new DelegateCommand(SeeMenu, OrderSelected);

            LoadAllOrders();
        }

        private void CancelOrder(object parameter)
        {
            try
            {
                if (_selectedOrder.ConfirmedByAdmin == true)
                {
                    MessageBox.Show("To cancel confirmed order you should contact restaurant's staff over phone or email");
                }
                else
                {
                    _cancelOrder.Remove(_selectedOrder, Username);
                }
            }
            catch (ItemNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadAllOrders();
            }
        }

        private void LoadAllOrders()
        {
            Orders = new List<OrderEntity>(_getOrders.GetAllOrders(Username));
            OnPropertyChanged(nameof(Orders));
        }

        private void OnCurrentAccountChanged()
        {
            OnPropertyChanged(nameof(Username));
        }

        private bool OrderSelected(object parameter)
        {
            return _selectedOrder != null && Orders.Count != 0;
        }

        private void SeeMenu(object obj)
        {
            _restaurantStore.CurrentRestaurant = _selectedOrder.Restaurant;
            _orderReportNavigator.Navigate();
        }

        public override void Dispose()
        {
            _accountStore.CurrentAccountChanged -= OnCurrentAccountChanged;
            base.Dispose();
        }
    }
}