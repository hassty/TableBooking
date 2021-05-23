using Autofac;
using AutoMapper;
using Core.Contracts;
using Core.Contracts.DataAccess;
using Core.UseCases;
using DataAccess.Database;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
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
            builder.RegisterAssemblyTypes(Assembly.Load(nameof(DataAccess)))
                .Where(t => t.Namespace.Contains("Database") && t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            // Use Cases
            builder.RegisterType<Sha256HashPasswordStrategy>().As<IPasswordProtectionStrategy>();
            builder.RegisterAssemblyTypes(Assembly.Load(nameof(Core)))
                .Where(t => t.Namespace.Contains("UseCases"))
                .AsSelf();

            // Mvvm
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace.Contains("Views"))
                .AsSelf();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Namespace.Contains("ViewModels"))
                .AsSelf();

            builder.RegisterType<MainWindow>().AsSelf();

            builder.RegisterType<ViewFactory>().As<IViewFactory>();

            return builder.Build();
        }
    }
}