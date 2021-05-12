using System.Windows.Input;
using TableBooking.Commands;
using WpfUI.Views;

namespace TableBooking.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IViewFactory _viewFactory;
        private DelegateCommand _changeViewCommand;

        private IView _selectedView;

        public ICommand ChangeViewCommand => _changeViewCommand ??= new DelegateCommand(
            viewName => SelectedView = _viewFactory.CreateView(viewName.ToString())
        );

        public IView SelectedView
        {
            get => _selectedView;
            set
            {
                _selectedView = value;
                OnPropertyChanged(nameof(SelectedView));
            }
        }

        public MainWindowViewModel(IViewFactory viewFactory)
        {
            _viewFactory = viewFactory;
        }
    }
}