using System;
using System.Collections.Generic;
using System.Text;
using WpfUI.Stores;
using WpfUI.ViewModels;

namespace WpfUI.Services
{
    public class NavigationService<TViewModel> : INavigationService where TViewModel : ViewModelBase
    {
        private readonly Func<TViewModel> _createViewModel;
        private readonly NavigationStore _navigationStore;

        public NavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}