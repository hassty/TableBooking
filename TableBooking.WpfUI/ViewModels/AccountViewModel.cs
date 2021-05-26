using AutoMapper;
using Core.UseCases;
using System.Collections.Generic;
using System.Windows.Input;
using WpfUI.Commands;
using WpfUI.Models;
using WpfUI.Services;
using WpfUI.Stores;

namespace WpfUI.ViewModels
{
    public class AccountViewModel : ViewModelBase
    {
        private readonly CurrentUserStore _accountStore;
        private readonly GetCustomerOrders _getOrders;
        private readonly IMapper _mapper;

        public ICommand NavigateHomeCommand { get; }
        public List<OrderModel> Orders { get; set; }
        public string Password => _accountStore.CurrentUser?.Password;
        public string Username => _accountStore.CurrentUser?.Username;

        public AccountViewModel(
            CurrentUserStore accountStore,
            INavigationService homeNavigationService,
            GetCustomerOrders getOrders,
            IMapper mapper
        )
        {
            _accountStore = accountStore;
            _getOrders = getOrders;
            _mapper = mapper;
            NavigateHomeCommand = new NavigateCommand(homeNavigationService);
            _accountStore.CurrentAccountChanged += OnCurrentAccountChanged;

            Orders = new List<OrderModel>();
            LoadAllOrders();
        }

        private void LoadAllOrders()
        {
            Orders.AddRange(_mapper.Map<List<OrderModel>>(_getOrders.GetAllOrders(Username)));
        }

        private void OnCurrentAccountChanged()
        {
            OnPropertyChanged(nameof(Password));
            OnPropertyChanged(nameof(Username));
        }

        public override void Dispose()
        {
            _accountStore.CurrentAccountChanged -= OnCurrentAccountChanged;
            base.Dispose();
        }
    }
}