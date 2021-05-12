using Core.UseCases;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TableBooking.Commands;
using TableBooking.Models;

namespace TableBooking.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly UserAuthorizationInteractor _userAuthorization;
        private DelegateCommand _loginCommand;
        private UserModel _user;
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

        public LoginViewModel(UserAuthorizationInteractor userAuthorizationInteractor)
        {
            _userAuthorization = userAuthorizationInteractor;
            _user = new UserModel();
        }

        private void Login(object obj)
        {
            if (obj is PasswordBox passwordBox)
            {
                _user.Password = passwordBox.Password;
                var successfullyLoggedIn = _userAuthorization.CheckLoginCredentials(_user.Username, _user.Password);
                if (successfullyLoggedIn)
                {
                    MessageBox.Show("success");
                }
                else
                {
                    MessageBox.Show("invalid credentials");
                }
            }
        }
    }
}