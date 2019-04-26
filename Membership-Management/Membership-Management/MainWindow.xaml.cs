using System;
using System.Windows;
using System.Windows.Navigation;

namespace Membership_Management
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainFrame.NavigationService.Navigating += NavigationService_Navigating;
            MainFrame.Navigate(new Uri("Login.xaml", UriKind.Relative));
        }

        private void NavigationService_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.Uri.OriginalString == "Login.xaml")
                dpMenu.Visibility = Visibility.Collapsed;
            else
                dpMenu.Visibility = Visibility.Visible;
        }
        
        private void BtnLock_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("Login.xaml", UriKind.Relative));
        }
    }
}
