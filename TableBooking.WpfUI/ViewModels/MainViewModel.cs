using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using WpfUI.Stores;
using WpfUI.Views;

namespace WpfUI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly DataTemplateManager _dataTemplateManager;
        private readonly NavigationStore _navigationStore;

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public MainViewModel(NavigationStore navigationStore, DataTemplateManager dataTemplateManager)
        {
            _navigationStore = navigationStore;
            _dataTemplateManager = dataTemplateManager;

            try
            {
                BindViewmodelsToViews();
            }
            catch (Exception)
            {
                MessageBox.Show("Could not bind view to its corresponding viewmodel");
            }

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void BindViewmodelsToViews()
        {
            var viewmodels = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsClass && t.Namespace.Contains("ViewModels")).ToArray();
            var views = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsClass && t.Namespace.Contains("Views")).ToArray();

            foreach (var view in views)
            {
                _dataTemplateManager.RegisterDataTemplate(viewmodels.FirstOrDefault(vm => vm.Name.Contains(view.Name)), view);
            }
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}