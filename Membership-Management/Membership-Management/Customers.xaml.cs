using Membership_Management.Services;
using System.Windows.Controls;

namespace Membership_Management
{
    /// <summary>
    /// Interaction logic for Customers.xaml
    /// </summary>
    public partial class Customers : Page
    {
        private DatabaseService databaseService = new DatabaseService();

        public Customers()
        {
            InitializeComponent();
            dgCustomers.ItemsSource = databaseService.GetAllCustomers();
        }
    }
}
