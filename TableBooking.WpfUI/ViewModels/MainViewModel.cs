using System.Windows.Input;
using WpfUI.Commands;
using WpfUI.Stores;
using WpfUI.Views;

namespace WpfUI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly NavigationStore _navigationStore;

        public ViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public bool IsOpen => _modalNavigationStore.IsOpen;

        public MainViewModel(NavigationStore navigationStore, ModalNavigationStore modalNavigationStore)
        {
            _navigationStore = navigationStore;
            _modalNavigationStore = modalNavigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _modalNavigationStore.CurrentViewModelChanged += OnCurrentModalViewModelChanged;
        }

        private void OnCurrentModalViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModel));
            OnPropertyChanged(nameof(IsOpen));
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}