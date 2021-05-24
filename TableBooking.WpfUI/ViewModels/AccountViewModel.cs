using System.Windows.Input;
using WpfUI.Commands;
using WpfUI.Services;
using WpfUI.Stores;

namespace WpfUI.ViewModels
{
    public class AccountViewModel : ViewModelBase
    {
        private readonly CurrentUserStore _accountStore;
        public ICommand NavigateHomeCommand { get; }
        public string Password => _accountStore.CurrentUser?.Password;
        public string Username => _accountStore.CurrentUser?.Username;

        public AccountViewModel(CurrentUserStore accountStore, INavigationService homeNavigationService)
        {
            _accountStore = accountStore;
            NavigateHomeCommand = new NavigateCommand(homeNavigationService);
            _accountStore.CurrentAccountChanged += OnCurrentAccountChanged;
        }

        private void OnCurrentAccountChanged()
        {
            OnPropertyChanged(nameof(Password));
            OnPropertyChanged(nameof(Username));
        }

        public override void Dispose()
        {
            _accountStore.CurrentAccountChanged -= OnCurrentAccountChanged;
            base.Dispose();
        }
    }
}