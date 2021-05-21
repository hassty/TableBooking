using Autofac;
using AutoMapper;
using Core.Contracts;
using Core.Contracts.DataAccess;
using Core.UseCases;
using DataAccess.Database;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using TableBooking.ViewModels;
using WpfUI;
using WpfUI.ViewModels;
using WpfUI.Views;

namespace TableBooking.UI
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            // Ef Core
            builder.Register(x =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<TableBookingContext>();
                optionsBuilder.UseInMemoryDatabase("Wpf");
                //optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);
                return new TableBookingContext(optionsBuilder.Options);
            }).As<DbContext>().SingleInstance();

            // AutoMapper
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<WpfMappingProfile>();
            })).AsSelf().SingleInstance();
            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                //config.AssertConfigurationIsValid();
                return config.CreateMapper(context.Resolve);
            }).As<IMapper>().InstancePerLifetimeScope();

            // Repositories
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
            builder.RegisterType<AdminRepository>().As<IAdminRepository>();
            builder.RegisterType<RestaurantRepository>().As<IRestaurantRepository>().SingleInstance();

            // Use Cases
            builder.RegisterType<Sha256HashPasswordStrategy>().As<IPasswordProtectionStrategy>();
            builder.RegisterType<RegisterCustomer>().AsSelf();
            builder.RegisterType<RestaurantsInteractor>().AsSelf();
            builder.RegisterType<LoginUser>().AsSelf();

            // Mvvm
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainWindowViewModel>().AsSelf();

            builder.RegisterType<LoginView>().AsSelf();
            builder.RegisterType<LoginViewModel>().AsSelf();
            builder.RegisterType<RegisterView>().AsSelf();
            builder.RegisterType<RegisterViewModel>().AsSelf();
            builder.RegisterType<RestaurantsView>().AsSelf();
            builder.RegisterType<RestaurantsViewModel>().AsSelf();
            builder.RegisterType<UsersView>().AsSelf().InstancePerDependency();
            builder.RegisterType<UsersViewModel>().AsSelf().InstancePerDependency();

            builder.RegisterType<ViewFactory>().As<IViewFactory>();

            return builder.Build();
        }
    }
}