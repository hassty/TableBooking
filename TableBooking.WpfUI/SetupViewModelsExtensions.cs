using Core.UseCases;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TableBooking;
using WpfUI.Services;
using WpfUI.Stores;
using WpfUI.ViewModels;

namespace WpfUI
{
    public static class SetupViewModelsExtensions
    {
        private static INavigationService CreateLayoutNavigationService<TViewModel>(IServiceProvider serviceProvider)
            where TViewModel : ViewModelBase
        {
            return new LayoutNavigationService<TViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<TViewModel>(),
                () => serviceProvider.GetRequiredService<NavigationBarViewModel>());
        }

        private static NavigationBarViewModel CreateNavigationBarViewModel(IServiceProvider serviceProvider)
        {
            return new NavigationBarViewModel(serviceProvider.GetRequiredService<CurrentUserStore>(),
                CreateLayoutNavigationService<HomeViewModel>(serviceProvider),
                CreateLayoutNavigationService<AccountViewModel>(serviceProvider),
                CreateLayoutNavigationService<LoginViewModel>(serviceProvider),
                CreateLayoutNavigationService<RegisterViewModel>(serviceProvider));
        }

        private static INavigationService CreateNavigationService<TViewModel>(IServiceProvider serviceProvider)
            where TViewModel : ViewModelBase
        {
            return new NavigationService<TViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<TViewModel>());
        }

        public static IServiceCollection SetupMainViewModel<TViewModel>(this IServiceCollection services)
            where TViewModel : ViewModelBase
        {
            services.AddSingleton(s => CreateLayoutNavigationService<TViewModel>(s));

            services.AddSingleton<MainViewModel>();
            services.AddSingleton(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainViewModel>()
            });

            return services;
        }

        public static IServiceCollection SetupViewModels(this IServiceCollection services)
        {
            services.AddTransient(s => new HomeViewModel(
                s.GetRequiredService<CurrentRestaurantStore>(),
                s.GetRequiredService<CurrentUserStore>(),
                s.GetRequiredService<GetRestaurants>(),
                CreateNavigationService<AddOrderViewModel>(s)));

            services.AddTransient(s => new AccountViewModel(
                s.GetRequiredService<CurrentUserStore>(),
                CreateLayoutNavigationService<HomeViewModel>(s),
                s.GetRequiredService<GetCustomerOrders>(),
                s.GetRequiredService<CancelOrder>()));

            services.AddTransient(s => new LoginViewModel(
                s.GetRequiredService<CurrentUserStore>(),
                CreateLayoutNavigationService<AccountViewModel>(s),
                CreateNavigationService<UnconfirmedOrdersViewModel>(s),
                CreateLayoutNavigationService<RegisterViewModel>(s),
                s.GetRequiredService<LoginUser>()));

            services.AddTransient(s => new RegisterViewModel(
                s.GetRequiredService<CurrentUserStore>(),
                CreateLayoutNavigationService<HomeViewModel>(s),
                CreateLayoutNavigationService<LoginViewModel>(s),
                s.GetRequiredService<RegisterCustomer>()));

            services.AddTransient(s => new AddOrderViewModel(
                s.GetRequiredService<CurrentRestaurantStore>(),
                s.GetRequiredService<CurrentUserStore>(),
                s.GetRequiredService<RestaurantInteractor>(),
                s.GetRequiredService<AddOrder>(),
                CreateLayoutNavigationService<AccountViewModel>(s)));

            services.AddTransient(s => new UnconfirmedOrdersViewModel(
                s.GetRequiredService<GetAllUnconfirmedOrders>(),
                s.GetRequiredService<ConfirmOrder>()));

            services.AddTransient(s => new RestaurantsViewModel(
                s.GetRequiredService<GetRestaurants>()
                ));

            services.AddTransient(CreateNavigationBarViewModel);

            return services;
        }
    }
}