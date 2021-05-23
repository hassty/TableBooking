using Core.Exceptions;
using Core.UseCases;
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
            RegisterCommand = new RegisterCommand(this, accountStore, homeNavigationService, registerCustomer);
        }
    }
}