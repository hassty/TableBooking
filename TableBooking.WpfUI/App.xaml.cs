using Core.Contracts;
using Core.Contracts.DataAccess;
using Core.UseCases;
using DataAccess.Database;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
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
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();

            services.AddSingleton<CurrentUserStore>();
            services.AddSingleton<CurrentRestaurantStore>();
            services.AddSingleton<NavigationStore>();

            services.AddSingleton<AddOrder>();
            services.AddSingleton<CancelOrder>();
            services.AddSingleton<ConfirmOrder>();
            services.AddSingleton<GetRestaurants>();
            services.AddSingleton<GetCustomerOrders>();
            services.AddSingleton<GetAllUnconfirmedOrders>();
            services.AddSingleton<LoginUser>();
            services.AddSingleton<RegisterAdmin>();
            services.AddSingleton<RegisterCustomer>();
            services.AddSingleton<RestaurantInteractor>();

            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            services.AddSingleton<IAdminRepository, AdminRepository>();
            services.AddSingleton<IRestaurantRepository, RestaurantRepository>();
            services.AddSingleton<IOrderRepository, OrderRepository>();
            services.AddSingleton<IPasswordProtectionStrategy, Sha256HashPasswordStrategy>();
            services.AddSingleton<INotifier, EmailNotifier>(s => new EmailNotifier(
                _configuration["Smtp:Username"],
                _configuration["Smtp:Password"],
                _configuration["Smtp:Host"]
            ));
            //services.AddSingleton<INotifier, FakeNotifier>();

            //services.AddDbContext<DbContext, TableBookingContext>(o => o.UseInMemoryDatabase("Wpf").EnableSensitiveDataLogging());
            services.AddDbContext<DbContext, TableBookingContext>(o => o.UseSqlServer(
                _configuration.GetConnectionString("SqlServerDB")
            ));

            services.AddSingleton(s => CreateHomeNavigationService(s));

            services.AddTransient(s => new HomeViewModel(
                s.GetRequiredService<CurrentRestaurantStore>(),
                s.GetRequiredService<CurrentUserStore>(),
                s.GetRequiredService<GetRestaurants>(),
                CreateAddOrderNavigatonService(s)));
            services.AddTransient(s => new AccountViewModel(
                s.GetRequiredService<CurrentUserStore>(),
                CreateHomeNavigationService(s),
                s.GetRequiredService<GetCustomerOrders>(),
                s.GetRequiredService<CancelOrder>()));
            services.AddTransient(s => new LoginViewModel(
                s.GetRequiredService<CurrentUserStore>(),
                CreateAccountNavigationService(s),
                CreateUnconfirmedOrdersNavigationService(s),
                CreateRegisterNavigationService(s),
                s.GetRequiredService<LoginUser>()));
            services.AddTransient(s => new RegisterViewModel(
                s.GetRequiredService<CurrentUserStore>(),
                CreateHomeNavigationService(s),
                CreateLoginNavigationService(s),
                s.GetRequiredService<RegisterCustomer>()));
            services.AddTransient(s => new AddOrderViewModel(
                s.GetRequiredService<CurrentRestaurantStore>(),
                s.GetRequiredService<CurrentUserStore>(),
                s.GetRequiredService<RestaurantInteractor>(),
                s.GetRequiredService<AddOrder>(),
                CreateAccountNavigationService(s)));
            services.AddTransient(s => new UnconfirmedOrdersViewModel(
                s.GetRequiredService<GetAllUnconfirmedOrders>(),
                s.GetRequiredService<ConfirmOrder>()));
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

        private INavigationService CreateAddOrderNavigatonService(IServiceProvider serviceProvider)
        {
            return new NavigationService<AddOrderViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<AddOrderViewModel>());
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
            return new NavigationService<LoginViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<LoginViewModel>());
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
            return new NavigationService<RegisterViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<RegisterViewModel>());
        }

        private INavigationService CreateUnconfirmedOrdersNavigationService(IServiceProvider serviceProvider)
        {
            return new NavigationService<UnconfirmedOrdersViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<UnconfirmedOrdersViewModel>());
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