using Core.UseCases;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TableBooking.Commands;
using TableBooking.Models;
using TableBooking.ViewModels;

namespace WpfUI.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly UserAuthorizationInteractor _userAuthorization;
        private DelegateCommand _registerCommand;
        private UserModel _user;
        public ICommand RegisterCommand => _registerCommand ??= new DelegateCommand(Register);

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

        public RegisterViewModel(UserAuthorizationInteractor userAuthorization)
        {
            _userAuthorization = userAuthorization;
            _user = new UserModel();
        }

        private void Register(object obj)
        {
            if (obj is PasswordBox passwordBox)
            {
                _user.Password = passwordBox.Password;
                var successfullyRegistered = _userAuthorization.Register(_user.Username, _user.Password);
                if (successfullyRegistered)
                {
                    MessageBox.Show("success");
                }
                else
                {
                    MessageBox.Show("username already exists");
                }
            }
        }
    }
}