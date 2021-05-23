using System;
using System.Collections.Generic;
using System.Text;
using WpfUI.Stores;
using WpfUI.ViewModels;

namespace WpfUI.Services
{
    public class ParameterNavigationService<TParameter, TViewModel> where TViewModel : ViewModelBase
    {
        private readonly Func<TParameter, TViewModel> _createViewModel;
        private readonly NavigationStore _navigationStore;

        public ParameterNavigationService(NavigationStore navigationStore, Func<TParameter, TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate(TParameter parameter)
        {
            _navigationStore.CurrentViewModel = _createViewModel(parameter);
        }
    }
}