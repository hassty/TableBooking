using Autofac;
using AutoMapper;
using Core.Contracts;
using Core.UseCases;
using DataAccess;
using DataAccess.Database;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace TableBooking.ConsoleUI
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>().As<IApplication>();

            builder.Register(_ =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<TableBookingContext>();
                optionsBuilder.UseInMemoryDatabase("Console");

                return new TableBookingContext(optionsBuilder.Options);
            }).As<DbContext>().SingleInstance();

            builder.RegisterType<UserAuthorizationInteractor>().AsSelf();
            builder.RegisterType<UserRepository>().As<IUserRepository>().SingleInstance();

            builder.RegisterType<RestaurantsInteractor>().AsSelf();
            builder.RegisterType<RestaurantRepository>().As<IRestaurantRepository>().SingleInstance();

            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DataAccessMappingProfile>();
            })).AsSelf().SingleInstance();
            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                config.AssertConfigurationIsValid();
                return config.CreateMapper(context.Resolve);
            }).As<IMapper>().InstancePerLifetimeScope();

            return builder.Build();
        }
    }
}