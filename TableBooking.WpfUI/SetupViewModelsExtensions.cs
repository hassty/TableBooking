using Core.UseCases;
using Microsoft.Extensions.DependencyInjection;
using System;
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
            return new NavigationBarViewModel(
                serviceProvider.GetRequiredService<CurrentUserStore>(),
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
            services.AddSingleton<DataTemplateManager>();

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
                CreateNavigationService<AddOrderViewModel>(s)
                ));

            services.AddTransient(s => new AccountViewModel(
                s.GetRequiredService<CurrentUserStore>(),
                s.GetRequiredService<CurrentRestaurantStore>(),
                CreateLayoutNavigationService<HomeViewModel>(s),
                CreateNavigationService<MenuItemsReportViewModel>(s),
                s.GetRequiredService<GetCustomerOrders>(),
                s.GetRequiredService<CancelOrder>()
                ));

            services.AddTransient(s => new LoginViewModel(
                s.GetRequiredService<CurrentUserStore>(),
                CreateLayoutNavigationService<AccountViewModel>(s),
                CreateNavigationService<UnconfirmedOrdersViewModel>(s),
                CreateLayoutNavigationService<RegisterViewModel>(s),
                s.GetRequiredService<LoginUser>()
                ));

            services.AddTransient(s => new RegisterViewModel(
                s.GetRequiredService<CurrentUserStore>(),
                CreateLayoutNavigationService<HomeViewModel>(s),
                CreateLayoutNavigationService<LoginViewModel>(s),
                s.GetRequiredService<RegisterCustomer>()
                ));

            services.AddTransient(s => new AddOrderViewModel(
                s.GetRequiredService<CurrentRestaurantStore>(),
                s.GetRequiredService<CurrentUserStore>(),
                s.GetRequiredService<AddOrder>(),
                s.GetRequiredService<GetRestaurants>(),
                s.GetRequiredService<GetRestaurantMenuItems>(),
                CreateLayoutNavigationService<AccountViewModel>(s),
                CreateLayoutNavigationService<HomeViewModel>(s)
                ));

            services.AddTransient(s => new UnconfirmedOrdersViewModel(
                s.GetRequiredService<CurrentUserStore>(),
                s.GetRequiredService<GetAllUnconfirmedOrders>(),
                s.GetRequiredService<ConfirmOrder>(),
                CreateNavigationService<RestaurantsViewModel>(s),
                CreateLayoutNavigationService<HomeViewModel>(s)
                ));

            services.AddTransient(s => new RestaurantsViewModel(
                s.GetRequiredService<CurrentRestaurantStore>(),
                s.GetRequiredService<GetRestaurants>(),
                s.GetRequiredService<RemoveRestaurant>(),
                CreateNavigationService<UnconfirmedOrdersViewModel>(s),
                CreateNavigationService<AddMenuItemsViewModel>(s),
                CreateNavigationService<AddRestaurantViewModel>(s),
                CreateNavigationService<UpdateRestaurantViewModel>(s)
                ));

            services.AddTransient(s => new AddRestaurantViewModel(
                s.GetRequiredService<AddRestaurant>(),
                CreateNavigationService<RestaurantsViewModel>(s)
                ));

            services.AddTransient(s => new UpdateRestaurantViewModel(
                s.GetRequiredService<CurrentRestaurantStore>(),
                s.GetRequiredService<UpdateRestaurant>(),
                s.GetRequiredService<CancelRestaurantChanges>(),
                CreateNavigationService<RestaurantsViewModel>(s)
                ));

            services.AddTransient(s => new AddMenuItemsViewModel(
                s.GetRequiredService<CurrentRestaurantStore>(),
                s.GetRequiredService<AddMenuItem>(),
                CreateNavigationService<RestaurantsViewModel>(s)
                ));

            services.AddTransient(s => new MenuItemsReportViewModel(
                s.GetRequiredService<GetRestaurantMenuItems>(),
                s.GetRequiredService<CurrentRestaurantStore>(),
                CreateNavigationService<AccountViewModel>(s)
                ));

            services.AddTransient(CreateNavigationBarViewModel);

            return services;
        }
    }
}