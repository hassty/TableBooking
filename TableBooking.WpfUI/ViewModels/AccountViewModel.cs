using Core.Entities;
using Core.Exceptions;
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
    public class AccountViewModel : ViewModelBase
    {
        private readonly CurrentUserStore _accountStore;
        private readonly CancelOrder _cancelOrder;
        private readonly GetCustomerOrders _getOrders;
        private OrderEntity _selectedOrder;
        public ICommand CancelSelectedOrderCommand { get; }
        public ICommand ConfirmOrderCommand { get; }
        public bool IsCustomer => _accountStore.IsCustomer;
        public ICommand NavigateHomeCommand { get; }
        public List<OrderEntity> Orders { get; set; }

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
            INavigationService homeNavigationService,
            GetCustomerOrders getOrders,
            CancelOrder cancelOrder
        )
        {
            _accountStore = accountStore;
            _getOrders = getOrders;
            _cancelOrder = cancelOrder;

            _accountStore.CurrentAccountChanged += OnCurrentAccountChanged;

            NavigateHomeCommand = new NavigateCommand(homeNavigationService);
            CancelSelectedOrderCommand = new DelegateCommand(CancelOrder, CanCancelOrder);

            LoadAllOrders();
        }

        private bool CanCancelOrder(object parameter)
        {
            return _selectedOrder != null && Orders.Count != 0;
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
            catch (EntityNotFoundException ex)
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

        public override void Dispose()
        {
            _accountStore.CurrentAccountChanged -= OnCurrentAccountChanged;
            base.Dispose();
        }
    }
}