using Autofac;
using AutoMapper;
using Core.Contracts;
using Core.UseCases;
using DataAccess;
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
                return new TableBookingContext(optionsBuilder.Options);
            }).As<DbContext>().SingleInstance();

            // AutoMapper
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DataAccessMappingProfile>();
                cfg.AddProfile<WpfMappingProfile>();
            })).AsSelf().SingleInstance();
            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            }).As<IMapper>().InstancePerLifetimeScope();

            // Use Cases
            builder.RegisterType<UserAuthorizationInteractor>().AsSelf();
            builder.RegisterType<UserRepository>().As<IUserRepository>().SingleInstance();
            builder.RegisterType<RestaurantsInteractor>().AsSelf();
            builder.RegisterType<RestaurantRepository>().As<IRestaurantRepository>().SingleInstance();

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