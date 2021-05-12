using System;
using System.Windows.Controls;
using WpfUI.ViewModels;

namespace WpfUI.Views
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : UserControl, IView
    {
        public RegisterView(RegisterViewModel registerViewModel)
        {
            InitializeComponent();
            DataContext = registerViewModel;
        }

        private void Content_TextChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            Register.IsEnabled = !String.IsNullOrWhiteSpace(Username.Text)
                && !String.IsNullOrWhiteSpace(PasswordBox.Password)
                && PasswordBox.Password.Equals(ConfirmPasswordBox.Password);
        }
    }
}