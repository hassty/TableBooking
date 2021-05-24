namespace WpfUI.ViewModels
{
    public class LayoutViewModel : ViewModelBase
    {
        public ViewModelBase ContentViewModel { get; }

        public NavigationBarViewModel NavigationBarViewModel { get; }

        public LayoutViewModel(NavigationBarViewModel navigationBarViewModel, ViewModelBase contentViewModel)
        {
            NavigationBarViewModel = navigationBarViewModel;
            ContentViewModel = contentViewModel;
        }

        public override void Dispose()
        {
            NavigationBarViewModel.Dispose();
            ContentViewModel.Dispose();

            base.Dispose();
        }
    }
}