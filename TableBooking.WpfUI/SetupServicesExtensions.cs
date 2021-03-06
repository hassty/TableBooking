using Core.Contracts;
using Core.Contracts.DataAccess;
using Core.Strategies;
using Core.UseCases;
using DataAccess;
using DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using WpfUI.Stores;

namespace WpfUI
{
    public static class SetupServicesExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            services.AddSingleton<IAdminRepository, AdminRepository>();
            services.AddSingleton<IRestaurantRepository, RestaurantRepository>();
            services.AddSingleton<IOrderRepository, OrderRepository>();

            return services;
        }

        public static IServiceCollection RegisterStores(this IServiceCollection services)
        {
            services.AddSingleton<CurrentUserStore>();
            services.AddSingleton<CurrentRestaurantStore>();
            services.AddSingleton<NavigationStore>();

            return services;
        }

        public static IServiceCollection RegisterUseCases(this IServiceCollection services)
        {
            services.AddSingleton<AddOrder>();
            services.AddSingleton<AddRestaurant>();
            services.AddSingleton<AddMenuItem>();
            services.AddSingleton<CancelOrder>();
            services.AddSingleton<CancelRestaurantChanges>();
            services.AddSingleton<ConfirmOrder>();
            services.AddSingleton<GetAllUnconfirmedOrders>();
            services.AddSingleton<GetCustomerOrders>();
            services.AddSingleton<GetRestaurants>();
            services.AddSingleton<GetRestaurantMenuItems>();
            services.AddSingleton<LoginUser>();
            services.AddSingleton<RegisterAdmin>();
            services.AddSingleton<RegisterCustomer>();
            services.AddSingleton<RemoveRestaurant>();
            services.AddSingleton<UpdateMenuItems>();
            services.AddSingleton<UpdateRestaurant>();

            return services;
        }

        public static IServiceCollection SetupDatabase(this IServiceCollection services)
        {
            services.AddDbContext<DbContext, TableBookingContext>(o => o.UseInMemoryDatabase("Wpf"));

            return services;
        }

        public static IServiceCollection SetupStrategies(this IServiceCollection services)
        {
            services.AddSingleton<IPasswordProtectionStrategy, Sha256HashPasswordStrategy>();
            services.AddSingleton<INotifier, FakeNotifier>();

            return services;
        }
    }
}