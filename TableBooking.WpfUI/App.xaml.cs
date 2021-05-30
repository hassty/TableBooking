using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using WpfUI;
using WpfUI.Services;
using WpfUI.ViewModels;

namespace TableBooking
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;

        private void ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services
                .RegisterStores()
                .RegisterUseCases()
                .RegisterRepositories()
                .SetupDatabase()
                .SetupStrategies()
                .SetupViewModels()
                .SetupMainViewModel<HomeViewModel>();

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            ConfigureServices();

            var initialNavigationService = _serviceProvider.GetRequiredService<INavigationService>();
            initialNavigationService.Navigate();

            MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}