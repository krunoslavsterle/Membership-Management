using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        }

        private void PbxPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var password = pbxPassword.Password;
            if (password == MASTER_PASSWORD)
                this.NavigationService.Navigate(new Uri("Home.xaml", UriKind.Relative));
        }
    }
}
