using System;
using System.Windows;
using System.Windows.Controls;
using TableBooking.ViewModels;

namespace WpfUI.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl, IView
    {
        public LoginView(LoginViewModel loginViewModel)
        {
            InitializeComponent();
            DataContext = loginViewModel;
        }

        private void Content_TextChanged(object sender, RoutedEventArgs e)
        {
            Login.IsEnabled = !String.IsNullOrWhiteSpace(Username.Text) && !String.IsNullOrWhiteSpace(PasswordBox.Password);
        }
    }
}