using Membership_Management.Services;
using System;
using System.Windows.Controls;

namespace Membership_Management
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        private DatabaseService databaseService = new DatabaseService();

        public Home()
        {
            InitializeComponent();
            var now = DateTime.Now;

            tbTotalMembers.Text = databaseService.GetCustomersCount(null).ToString();
            tbActiveMembers.Text = databaseService.GetCustomersCount(p => p.ValidUntil > DateTime.Now).ToString();
            tbExpireingMembers.Text = databaseService.GetCustomersCount(p => p.ValidUntil < DateTime.Now.AddDays(5) && p.ValidUntil >= DateTime.Now).ToString();
            tbNewMembers.Text = databaseService.GetCustomersCount(p => p.RegDate >= new DateTime(now.Year, now.Month, 1, 0, 0, 0)).ToString();
        }
    }
}
