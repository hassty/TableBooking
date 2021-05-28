using Core.Entities.Users;
using Core.Exceptions;
using Core.UseCases;
using System;
using System.Windows;
using System.Windows.Input;
using WpfUI.Commands;
using WpfUI.Dto;
using WpfUI.Services;
using WpfUI.Stores;

namespace WpfUI.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private readonly CurrentUserStore _accountStore;
        private readonly INavigationService _homeNavigationService;
        private readonly INavigationService _loginNavigationService;
        private readonly RegisterCustomer _registerCustomer;
        private string _confirmPassword;
        private string _password;
        private string _username;

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                if (_confirmPassword != value)
                {
                    _confirmPassword = value;
                    OnPropertyChanged(nameof(ConfirmPassword));
                }
            }
        }

        public ICommand GoToLoginCommand { get; }

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

        public ICommand RegisterCommand { get; }

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

        public RegisterViewModel(
            CurrentUserStore accountStore,
            INavigationService homeNavigationService,
            INavigationService loginNavigationService,
            RegisterCustomer registerCustomer
        )
        {
            _accountStore = accountStore;
            _homeNavigationService = homeNavigationService;
            _loginNavigationService = loginNavigationService;
            _registerCustomer = registerCustomer;

            RegisterCommand = new DelegateCommand(Register, CanRegister);
            GoToLoginCommand = new DelegateCommand(GoToLogin);
        }

        private bool CanRegister(object arg)
        {
            return !String.IsNullOrWhiteSpace(_username)
                && !String.IsNullOrWhiteSpace(_password) && !String.IsNullOrWhiteSpace(_confirmPassword)
                && _password == _confirmPassword;
        }

        private void GoToLogin(object parameter)
        {
            _loginNavigationService.Navigate();
        }

        private void Register(object obj)
        {
            var customer = new CustomerDto()
            {
                Username = _username,
                Password = _password
            };

            try
            {
                _accountStore.CurrentUser = _registerCustomer.Register(customer);
                _homeNavigationService.Navigate();
            }
            catch (UserAlreadyExistsException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}