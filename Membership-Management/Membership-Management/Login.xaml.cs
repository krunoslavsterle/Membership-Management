using Membership_Management.Infrastructure.Helpers;
using Membership_Management.Infrastructure.Services;
using System;
using System.Threading;
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

            var syncServie = new SyncService();
            var aq = syncServie.AquireLock();
          //  var removed = syncServie.RemoveLock();

            //TaskHelper.RunPeriodicAsync(GoogleCloudHelper.ListFiles, TimeSpan.FromSeconds(5), TimeSpan.FromHours(2), CancellationToken.None);
        }

        private void PbxPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var password = pbxPassword.Password;
            if (password == MASTER_PASSWORD)
                this.NavigationService.Navigate(new Uri("Home.xaml", UriKind.Relative));
        }
    }
}
