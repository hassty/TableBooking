using Core.Contracts;
using Core.Contracts.DataAccess;
using Core.UseCases;
using DataAccess;
using DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Windows;
using WpfUI;
using WpfUI.Services;
using WpfUI.Stores;
using WpfUI.ViewModels;

namespace TableBooking
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    public partial class App : Application
    {
        private IConfiguration _configuration;
        private IServiceProvider _serviceProvider;

        private void ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            services
                .RegisterStores()
                .RegisterUseCases()
                .RegisterRepositories()
                .SetupDatabase(_configuration)
                .SetupStrategies()
                .SetupViewModels()
                .SetupMainViewModel<RestaurantDetailsViewModel>();

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