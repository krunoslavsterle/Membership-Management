using System;
using System.Windows;
using System.Windows.Controls;

namespace Membership_Management
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        private const String MASTER_PASSWORD = "1234";

        public Login()
        {
            InitializeComponent();
            pbxPassword.Focus();
        }

        private void PbxPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var password = pbxPassword.Password;
            if (password == MASTER_PASSWORD)
                this.NavigationService.Navigate(new Uri("Home.xaml", UriKind.Relative));
        }
    }
}
