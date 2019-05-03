using Membership_Management.Infrastructure.Helpers;
using Membership_Management.Services;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Membership_Management
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DatabaseService databaseService = new DatabaseService();
        private EmailNotificationService emailNotificationService = new EmailNotificationService();

        public MainWindow()
        {
            InitializeComponent();
            databaseService.ValidateDatabaseExist();

            TaskHelper.RunPeriodicAsync(emailNotificationService.CheckSubscriptionsAndSendNotifications, TimeSpan.FromMinutes(5), TimeSpan.FromHours(2), CancellationToken.None);

            MainFrame.NavigationService.Navigating += NavigationService_Navigating;
            MainFrame.Navigate(new Uri("Login.xaml", UriKind.Relative));

        }

        private void NavigationService_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            dpMenu.Visibility = Visibility.Visible;
            if (e.Uri.OriginalString == "Login.xaml")
                dpMenu.Visibility = Visibility.Collapsed;

            HandleMenuButtons(e.Uri.OriginalString);
        }

        private void HandleMenuButtons(string pageUri)
        {
            var activeBrush = new SolidColorBrush(Color.FromRgb(15, 145, 200));

            btnCustomers.Background =
                btnHome.Background =
                btnLock.Background =
                btnSettings.Background = Brushes.Transparent;

            if (pageUri == "Home.xaml")
                btnHome.Background = activeBrush;
            else if (pageUri == "Settings.xaml")
                btnSettings.Background = activeBrush;
            else if (pageUri == "Customers.xaml")
                btnCustomers.Background = activeBrush;
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("Home.xaml", UriKind.Relative));
        }

        private void BtnCustomers_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("Customers.xaml", UriKind.Relative));
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("Settings.xaml", UriKind.Relative));
        }

        private void BtnLock_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("Login.xaml", UriKind.Relative));
        }
    }
}
