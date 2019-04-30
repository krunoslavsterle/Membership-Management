using Membership_Management.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Membership_Management
{
    public partial class Login : Page
    {
        private DatabaseService databaseService = new DatabaseService();
        private string masterPassword = null;

        public Login()
        {
            InitializeComponent();
            pbxPassword.Focus();
            masterPassword = databaseService.GetMasterPassword().Password;
        }

        private void PbxPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(masterPassword))
                MessageBox.Show("Can't find Master Password in the Database. Please contact the developer", "Master Passwrod Error");
            else
            {
                var password = pbxPassword.Password;
                if (password == masterPassword)
                    this.NavigationService.Navigate(new Uri("Home.xaml", UriKind.Relative));
            }
        }
    }
}
