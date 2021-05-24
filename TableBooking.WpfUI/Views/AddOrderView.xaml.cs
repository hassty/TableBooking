using System.Windows;
using System.Windows.Controls;

namespace WpfUI.Views
{
    /// <summary>
    /// Interaction logic for AddOrderView.xaml
    /// </summary>
    public partial class AddOrderView : UserControl
    {
        public AddOrderView()
        {
            InitializeComponent();
        }

        private void Calendar_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is Calendar calendar)
            {
                calendar.BlackoutDates.AddDatesInPast();
            }
        }
    }
}