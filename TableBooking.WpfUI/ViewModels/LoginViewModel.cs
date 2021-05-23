using AutoMapper;
using Core.Exceptions;
using Core.UseCases;
using System;
using System.ComponentModel;
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
        private readonly CurrentUserStore _accountStore;
        private readonly INavigationService _loginNavigationService;
        private readonly LoginUser _loginUser;
        private readonly IMapper _mapper;
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

        public LoginViewModel(
            CurrentUserStore accountStore,
            INavigationService loginNavigationService,
            IMapper mapper,
            LoginUser loginUser
        )
        {
            _accountStore = accountStore;
            _loginNavigationService = loginNavigationService;
            _mapper = mapper;
            _loginUser = loginUser;
            LoginCommand = new DelegateCommand(Login, CanLogin);
        }

        private bool CanLogin(object parameter)
        {
            return !String.IsNullOrWhiteSpace(_username) && !String.IsNullOrWhiteSpace(_password);
        }

        private void Login(object parameter)
        {
            try
            {
                var loggedInUser = _loginUser.Login(_username, _password);
                _accountStore.CurrentUser = _mapper.Map<UserModel>(loggedInUser);
                _loginNavigationService.Navigate();
            }
            catch (InvalidCredentialsException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}