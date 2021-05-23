using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WpfUI.Commands;
using WpfUI.Services;
using WpfUI.Stores;

namespace WpfUI.ViewModels
{
    public class AccountViewModel : ViewModelBase
    {
        private readonly AccountStore _accountStore;
        public string Email => _accountStore.CurrentAccount?.Email;
        public ICommand NavigateHomeCommand { get; }
        public string Username => _accountStore.CurrentAccount?.Username;

        public AccountViewModel(AccountStore accountStore, INavigationService homeNavigationService)
        {
            _accountStore = accountStore;
            NavigateHomeCommand = new NavigateCommand(homeNavigationService);
            _accountStore.CurrentAccountChanged += OnCurrentAccountChanged;
        }

        private void OnCurrentAccountChanged()
        {
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(Username));
        }

        public override void Dispose()
        {
            _accountStore.CurrentAccountChanged -= OnCurrentAccountChanged;
            base.Dispose();
        }
    }
}