using WpfUI.Services;
using WpfUI.Stores;

namespace WpfUI.Commands
{
    public class LogoutCommand : CommandBase
    {
        private readonly CurrentUserStore _accountStore;
        private readonly INavigationService _navigationService;

        public LogoutCommand(CurrentUserStore accountStore, INavigationService navigationService)
        {
            _accountStore = accountStore;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _accountStore.Logout();
            _navigationService.Navigate();
        }
    }
}