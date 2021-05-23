using Core.Exceptions;
using Core.UseCases;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfUI.Commands;
using WpfUI.Models;
using WpfUI.Services;
using WpfUI.Stores;

namespace WpfUI.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private readonly CurrentUserStore _accountStore;
        private readonly INavigationService _homeNavigationService;
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
            RegisterCustomer registerCustomer
        )
        {
            _accountStore = accountStore;
            _homeNavigationService = homeNavigationService;
            _registerCustomer = registerCustomer;

            RegisterCommand = new DelegateCommand(Register, CanRegister);
        }

        private bool CanRegister(object arg)
        {
            return !String.IsNullOrWhiteSpace(_username)
                && !String.IsNullOrWhiteSpace(_password) && !String.IsNullOrWhiteSpace(_confirmPassword)
                && _password == _confirmPassword;
        }

        private void Register(object obj)
        {
            var customer = new CustomerModel()
            {
                Username = _username,
                Password = _password
            };

            try
            {
                _registerCustomer.Register(customer);
                _accountStore.CurrentUser = customer;
                _homeNavigationService.Navigate();
            }
            catch (UserAlreadyExistsException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}