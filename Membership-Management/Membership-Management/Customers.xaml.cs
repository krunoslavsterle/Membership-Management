using Membership_Management.Infrastructure.Models;
using Membership_Management.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
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
