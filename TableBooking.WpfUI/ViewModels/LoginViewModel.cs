using Core.UseCases;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TableBooking.Commands;
using WpfUI.Models;

namespace TableBooking.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private DelegateCommand _loginCommand;
        private CustomerModel _user;
        public ICommand LoginCommand => _loginCommand ??= new DelegateCommand(Login);

        public string Username
        {
            get => _user.Username;
            set
            {
                if (_user.Username != value)
                {
                    _user.Username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        public LoginViewModel()
        {
            _user = new CustomerModel();
        }

        private void Login(object obj)
        {
            if (obj is PasswordBox passwordBox)
            {
                _user.Password = passwordBox.Password;
            }
        }
    }
}