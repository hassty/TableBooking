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
    public class LoginViewModel : ViewModelBase
    {
        private string _password;
        private string _username;

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

        public LoginViewModel(AccountStore accountStore, INavigationService loginNavigationService)
        {
            LoginCommand = new LoginCommand(this, accountStore, loginNavigationService);
        }
    }
}