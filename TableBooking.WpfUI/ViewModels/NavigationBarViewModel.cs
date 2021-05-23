using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WpfUI.Commands;
using WpfUI.Services;
using WpfUI.Stores;

namespace WpfUI.ViewModels
{
    public class NavigationBarViewModel : ViewModelBase
    {
        private readonly CurrentUserStore _accountStore;
        public bool IsLoggedIn => _accountStore.IsLoggedIn;
        public bool IsLoggedOut => !IsLoggedIn;
        public ICommand LogoutCommand { get; }
        public ICommand NavigateAccountCommand { get; }
        public ICommand NavigateHomeCommand { get; }
        public ICommand NavigateLoginCommand { get; }
        public ICommand NavigateRegisterCommand { get; }

        public NavigationBarViewModel(
            CurrentUserStore accountStore,
            INavigationService homeNavigationService,
            INavigationService accountNavigationService,
            INavigationService loginNavigationService,
            INavigationService registerNavigationService
        )
        {
            _accountStore = accountStore;
            NavigateHomeCommand = new NavigateCommand(homeNavigationService);
            NavigateAccountCommand = new NavigateCommand(accountNavigationService);
            NavigateLoginCommand = new NavigateCommand(loginNavigationService);
            NavigateRegisterCommand = new NavigateCommand(registerNavigationService);
            LogoutCommand = new LogoutCommand(_accountStore, homeNavigationService);

            _accountStore.CurrentAccountChanged += OnCurrentAccountChanged;
        }

        private void OnCurrentAccountChanged()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
        }

        public override void Dispose()
        {
            _accountStore.CurrentAccountChanged -= OnCurrentAccountChanged;

            base.Dispose();
        }
    }
}