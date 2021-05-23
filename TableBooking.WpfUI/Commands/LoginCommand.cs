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
        private readonly CurrentUserStore _accountStore;
        private readonly INavigationService _navigationService;
        private readonly LoginViewModel _viewModel;

        public LoginCommand(LoginViewModel viewModel, CurrentUserStore accountStore, INavigationService navigationService)
        {
            _viewModel = viewModel;
            _accountStore = accountStore;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            var accountModel = new UserModel
            {
                Username = _viewModel.Username,
                Password = _viewModel.Password
            };

            _accountStore.CurrentUser = accountModel;
            _navigationService.Navigate();
        }
    }
}