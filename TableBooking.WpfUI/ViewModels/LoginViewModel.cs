using Core.Exceptions;
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
        private readonly LoginUser _loginUser;
        private DelegateCommand _loginCommand;
        private CustomerModel _customer;
        public ICommand LoginCommand => _loginCommand ??= new DelegateCommand(Login);

        public string Username
        {
            get => _customer.Username;
            set
            {
                if (_customer.Username != value)
                {
                    _customer.Username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        public LoginViewModel(LoginUser loginUser)
        {
            _loginUser = loginUser;
            _customer = new CustomerModel();
        }

        private void Login(object obj)
        {
            if (obj is PasswordBox passwordBox)
            {
                _customer.Password = passwordBox.Password;
                try
                {
                    var user = _loginUser.Login(_customer.Username, _customer.Password);
                    MessageBox.Show("success");
                }
                catch (InvalidCredentialsException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}