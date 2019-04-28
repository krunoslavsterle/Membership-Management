using Membership_Management.Infrastructure.Models;
using Membership_Management.Services;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Membership_Management
{
    /// <summary>
    /// Interaction logic for Customers.xaml
    /// </summary>
    public partial class Customers : Page
    {
        private DatabaseService databaseService = new DatabaseService();
        private ObservableCollection<Customer> customers;
        private Customer editCustomer = null;

        public Customers()
        {
            InitializeComponent();

            var customerList = databaseService.GetAllCustomers().ToList();
            customers = new ObservableCollection<Customer>(customerList);

            dgCustomers.ItemsSource = customers;
            dgCustomers.CellEditEnding += DgCustomers_CellEditEnding;
        }

        private void DgCustomers_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var preUpdatedItem = e.Row.Item as Customer;
            var updatedItem = customers.First(p => p.Id == preUpdatedItem.Id);
        }

        private void BtnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var customer = dgCustomers.SelectedItem as Customer;
            var messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                databaseService.DeleteCustomer(customer.Id);
                customers.Remove(customer);
                //dgCustomers.Items.Refresh();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            editCustomer = ((FrameworkElement)sender).DataContext as Customer;

            dgCustomers.Visibility = Visibility.Collapsed;
            gridEditCustomer.Visibility = Visibility.Visible;

            var firstNameBinding = new Binding(nameof(Customer.FirstName));
            var lastNameBinding = new Binding(nameof(Customer.LastName));
            var emailBinding = new Binding(nameof(Customer.Email));
            var addressBinding = new Binding(nameof(Customer.Address));
            var validUntilBinding = new Binding(nameof(Customer.ValidUntil));
            
            firstNameBinding.Source = editCustomer;
            lastNameBinding.Source = editCustomer;
            emailBinding.Source = editCustomer;
            addressBinding.Source = editCustomer;
            validUntilBinding.Source = editCustomer;

            tbxEditFirstName.SetBinding(TextBox.TextProperty, firstNameBinding);
            tbxEditLastName.SetBinding(TextBox.TextProperty, lastNameBinding);
            tbxEditEmail.SetBinding(TextBox.TextProperty, emailBinding);
            tbxEditAddress.SetBinding(TextBox.TextProperty, addressBinding);
            dPickerEditExpiring.SetBinding(DatePicker.SelectedDateProperty, validUntilBinding);
        }

        private void BtnEditSave_Click(object sender, RoutedEventArgs e)
        {
            databaseService.UpdateCustomer(editCustomer);
            editCustomer = null;

            dgCustomers.Visibility = Visibility.Visible;
            gridEditCustomer.Visibility = Visibility.Collapsed;
        }

        private void BtnEditCancel_Click(object sender, RoutedEventArgs e)
        {
            editCustomer = null;

            dgCustomers.Visibility = Visibility.Visible;
            gridEditCustomer.Visibility = Visibility.Collapsed;
        }
    }
}
