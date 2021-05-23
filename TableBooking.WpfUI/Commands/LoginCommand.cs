using System;
using System.Collections.Generic;
using System.Text;
using WpfUI.Models;
using WpfUI.Services;
using WpfUI.Stores;
using WpfUI.ViewModels;

namespace WpfUI.Commands
{
    public class LoginCommand : CommandBase
    {
        private readonly AccountStore _accountStore;
        private readonly INavigationService _navigationService;
        private readonly LoginViewModel _viewModel;

        public LoginCommand(LoginViewModel viewModel, AccountStore accountStore, INavigationService navigationService)
        {
            _viewModel = viewModel;
            _accountStore = accountStore;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            var accountModel = new AccountModel
            {
                Email = $"{_viewModel.Username}@test.com",
                Username = _viewModel.Username
            };

            _accountStore.CurrentAccount = accountModel;
            _navigationService.Navigate();
        }
    }
}