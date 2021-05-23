using Core.Exceptions;
using Core.UseCases;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfUI.Commands;
using WpfUI.Models;

namespace WpfUI.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private readonly RegisterCustomer _registerCustomer;
        private DelegateCommand _registerCommand;
        private CustomerModel _user;
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

        public RegisterViewModel(RegisterCustomer registerCustomer)
        {
            _registerCustomer = registerCustomer;
            _user = new CustomerModel();
        }

        private void Register(object obj)
        {
            if (obj is PasswordBox passwordBox)
            {
                _user.Password = passwordBox.Password;
                try
                {

                    _registerCustomer.Register(_user);
                    MessageBox.Show("success");
                }
                catch (UserAlreadyExistsException ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}