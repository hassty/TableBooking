using AutoMapper;
using Core.Contracts;
using Core.Contracts.DataAccess;
using Core.UseCases;
using DataAccess.Database;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
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
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<CurrentUserStore>();
            services.AddSingleton<CurrentRestaurantStore>();
            services.AddSingleton<NavigationStore>();

            services.AddSingleton<RegisterCustomer>();
            services.AddSingleton<LoginUser>();
            services.AddSingleton<GetAllRestaurants>();
            services.AddSingleton<RestaurantInteractor>();
            services.AddSingleton<AddOrder>();

            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            services.AddSingleton<IAdminRepository, AdminRepository>();
            services.AddSingleton<IRestaurantRepository, RestaurantRepository>();
            services.AddSingleton<IOrderRepository, OrderRepository>();
            services.AddSingleton<IPasswordProtectionStrategy, Sha256HashPasswordStrategy>();

            //services.AddDbContext<DbContext, TableBookingContext>(o => o.UseInMemoryDatabase("Wpf").EnableSensitiveDataLogging());
            services.AddDbContext<DbContext, TableBookingContext>(o => o.UseSqlServer(
                ConfigurationManager.ConnectionStrings["SqlServerDB"].ConnectionString
            ));

            services.AddSingleton(s => new MapperConfiguration(cfg =>
              {
                  cfg.AddProfile<WpfMappingProfile>();
              })
            );
            services.AddSingleton(s =>
            {
                var config = s.GetRequiredService<MapperConfiguration>();
                config.AssertConfigurationIsValid();
                return config.CreateMapper();
            });

            services.AddSingleton(s => CreateHomeNavigationService(s));

            services.AddTransient(s => new HomeViewModel(
                s.GetRequiredService<CurrentRestaurantStore>(),
                s.GetRequiredService<CurrentUserStore>(),
                s.GetRequiredService<GetAllRestaurants>(),
                CreateAddOrderNavigatonService(s),
                s.GetRequiredService<IMapper>()));
            services.AddTransient(s => new AccountViewModel(
                s.GetRequiredService<CurrentUserStore>(),
                CreateHomeNavigationService(s)));
            services.AddTransient(s => new LoginViewModel(
                s.GetRequiredService<CurrentUserStore>(),
                CreateAccountNavigationService(s),
                CreateRegisterNavigationService(s),
                s.GetRequiredService<IMapper>(),
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
                CreateAccountNavigationService(s),
                s.GetRequiredService<IMapper>()
                ));
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