using Core.Contracts;
using Core.Contracts.DataAccess;
using Core.UseCases;
using DataAccess.Database;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
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
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<CurrentUserStore>();
            services.AddSingleton<NavigationStore>();

            services.AddSingleton<RegisterCustomer>();
            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            services.AddSingleton<IPasswordProtectionStrategy, Sha256HashPasswordStrategy>();
            services.AddDbContext<DbContext, TableBookingContext>(o => o.UseInMemoryDatabase("Wpf"));

            services.AddSingleton(s => CreateHomeNavigationService(s));

            services.AddTransient(s => new HomeViewModel(CreateLoginNavigationService(s)));
            services.AddTransient(s => new AccountViewModel(
                s.GetRequiredService<CurrentUserStore>(),
                CreateHomeNavigationService(s)));
            services.AddTransient(s => new LoginViewModel(
                s.GetRequiredService<CurrentUserStore>(),
                CreateAccountNavigationService(s)));
            services.AddTransient(s => new RegisterViewModel(
                s.GetRequiredService<CurrentUserStore>(),
                CreateHomeNavigationService(s),
                s.GetRequiredService<RegisterCustomer>()));
            services.AddTransient(CreateNavigationBarViewModel);
            services.AddSingleton<MainViewModel>();

            services.AddSingleton(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainViewModel>()
            });

            _serviceProvider = services.BuildServiceProvider();
        }

        private INavigationService CreateAccountNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<AccountViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<AccountViewModel>(),
                () => serviceProvider.GetRequiredService<NavigationBarViewModel>());
        }

        private INavigationService CreateHomeNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<HomeViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<HomeViewModel>(),
                () => serviceProvider.GetRequiredService<NavigationBarViewModel>());
        }

        private INavigationService CreateLoginNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<LoginViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<LoginViewModel>(),
                () => serviceProvider.GetRequiredService<NavigationBarViewModel>());
        }

        private NavigationBarViewModel CreateNavigationBarViewModel(IServiceProvider serviceProvider)
        {
            return new NavigationBarViewModel(serviceProvider.GetRequiredService<CurrentUserStore>(),
                CreateHomeNavigationService(serviceProvider),
                CreateAccountNavigationService(serviceProvider),
                CreateLoginNavigationService(serviceProvider),
                CreateRegisterNavigationService(serviceProvider));
        }

        private INavigationService CreateRegisterNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<RegisterViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<RegisterViewModel>(),
                () => serviceProvider.GetRequiredService<NavigationBarViewModel>());
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var initialNavigationService = _serviceProvider.GetRequiredService<INavigationService>();
            initialNavigationService.Navigate();

            MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}