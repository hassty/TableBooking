using Core.Exceptions;
using Core.UseCases;
using System;
using System.Windows;
using System.Windows.Input;
using WpfUI.Commands;
using WpfUI.Services;
using WpfUI.Stores;

namespace WpfUI.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly CurrentUserStore _accountStore;
        private readonly INavigationService _loginNavigationService;
        private readonly LoginUser _loginUser;
        private readonly INavigationService _registerNavigationService;
        private string _password;
        private string _username;
        public ICommand GoToRegisterCommand { get; }
        public ICommand LoginCommand { get; }

        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        public LoginViewModel(
            CurrentUserStore accountStore,
            INavigationService loginNavigationService,
            INavigationService registerNavigationService,
            LoginUser loginUser
        )
        {
            _accountStore = accountStore;
            _loginNavigationService = loginNavigationService;
            _registerNavigationService = registerNavigationService;
            _loginUser = loginUser;

            LoginCommand = new DelegateCommand(Login, CanLogin);
            GoToRegisterCommand = new DelegateCommand(GoToRegister);
        }

        private bool CanLogin(object parameter)
        {
            return !String.IsNullOrWhiteSpace(_username) && !String.IsNullOrWhiteSpace(_password);
        }

        private void GoToRegister(object parameter)
        {
            _registerNavigationService.Navigate();
        }

        private void Login(object parameter)
        {
            try
            {
                var loggedInUser = _loginUser.Login(_username, _password);
                _accountStore.CurrentUser = loggedInUser;
                _loginNavigationService.Navigate();
            }
            catch (InvalidCredentialsException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}