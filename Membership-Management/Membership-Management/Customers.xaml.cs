using Membership_Management.Infrastructure.Models;
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
    /// Interaction logic for Customers.xaml
    /// </summary>
    public partial class Customers : Page
    {
        public Customers()
        {
            InitializeComponent();

            dgCustomers.ItemsSource = GetCustomersMock();
        }

        public List<Customer> GetCustomersMock()
        {
            var list = new List<Customer>();
            list.Add(new Customer
            {
                Id = 1,
                RegNumber = 1,
                RegDate = DateTime.Now.AddDays(-40).ToShortDateString(),
                Address = "Street 111, 54000 Osijek",
                FirstName = "John",
                LastName = "Doe",
                Email = "mail@test.com",
                ValidUntil = DateTime.Now.AddDays(10).ToShortDateString()
            });

            list.Add(new Customer
            {
                Id = 2,
                RegNumber = 2,
                RegDate = DateTime.Now.AddDays(-40).ToShortDateString(),
                Address = "Street 131, 54000 Osijek",
                FirstName = "John",
                LastName = "Doe 2",
                Email = "mail@test.com",
                ValidUntil = DateTime.Now.AddDays(10).ToShortDateString()
            });

            list.Add(new Customer
            {
                Id = 3,
                RegNumber = 3,
                RegDate = DateTime.Now.AddDays(-40).ToShortDateString(),
                Address = "Street 131, 54000 Osijek",
                FirstName = "John 3",
                LastName = "Doe 2",
                Email = "mail@test.com",
                ValidUntil = DateTime.Now.AddDays(10).ToShortDateString()
            });

            return list;
        }
    }
}
