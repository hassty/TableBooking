using Core.Exceptions;
using Core.UseCases;
using System;
using System.Collections.Generic;
using System.Text;
using WpfUI.Models;
using WpfUI.Services;
using WpfUI.Stores;
using WpfUI.ViewModels;

namespace WpfUI.Commands
{
    public class RegisterCommand : CommandBase
    {
        private readonly CurrentUserStore _accountStore;
        private readonly INavigationService _navigationService;
        private readonly RegisterCustomer _registerCustomer;
        private readonly RegisterViewModel _viewModel;

        public RegisterCommand(
            RegisterViewModel viewModel,
            CurrentUserStore accountStore,
            INavigationService navigationService,
            RegisterCustomer registerCustomer
        )
        {
            _viewModel = viewModel;
            _accountStore = accountStore;
            _navigationService = navigationService;
            _registerCustomer = registerCustomer;
        }

        public override void Execute(object parameter)
        {
            var customer = new CustomerModel()
            {
                Username = _viewModel.Username,
                Password = _viewModel.Password
            };

            try
            {
                _registerCustomer.Register(customer);
            }
            catch (UserAlreadyExistsException)
            {
                throw;
            }

            _accountStore.CurrentUser = new UserModel
            {
                Username = _viewModel.Username,
                Password = _viewModel.Password
            };

            _navigationService.Navigate();
        }
    }
}