using Autofac;
using System.Windows;
using TableBooking.UI;

namespace TableBooking
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var container = ContainerConfig.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var mainWindow = scope.Resolve<MainWindow>();
                mainWindow.Show();
            }
        }
    }
}