using Membership_Management.Infrastructure.Models;
using Membership_Management.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Membership_Management
{
    public partial class Customers : Page
    {
        private enum SubView
        {
            Customers,
            Add,
            Edit
        }

        private DatabaseService databaseService = new DatabaseService();
        private ObservableCollection<Customer> customers;
        private Customer editCustomer = null;

        public Customers()
        {
            InitializeComponent();

            customers = new ObservableCollection<Customer>(databaseService.GetAllCustomers().ToList());
            dgCustomers.ItemsSource = customers;
        }
        
        // Events
        //

        private void BtnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var customer = dgCustomers.SelectedItem as Customer;
            var messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                databaseService.DeleteCustomer(customer.Id);
                customers.Remove(customer);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            editCustomer = ((FrameworkElement)sender).DataContext as Customer;

            SetEditCustomerBindings();
            SwitchSubView(SubView.Edit);
        }

        private void BtnEditSave_Click(object sender, RoutedEventArgs e)
        {
            if (customers.Count(p => p.Id == editCustomer.Id) > 0)
                databaseService.UpdateCustomer(editCustomer);
            else
            {
                databaseService.InsertCustomer(editCustomer);
                customers.Add(editCustomer);
            }

            editCustomer = null;
            SwitchSubView(SubView.Customers);
        }

        private void BtnEditCancel_Click(object sender, RoutedEventArgs e)
        {
            editCustomer = null;
            SwitchSubView(SubView.Customers);
        }

        private void BtnAddNew_Click(object sender, RoutedEventArgs e)
        {
            editCustomer = new Customer
            {
                Id = customers.OrderByDescending(p => p.Id).First().Id + 1,
                RegNumber = customers.OrderByDescending(p => p.RegNumber).First().RegNumber + 1,
                ValidUntil = DateTime.Now
            };

            SetEditCustomerBindings();
            SwitchSubView(SubView.Add);
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            ApplySearchFilter();
        }

        private void TbxSearch_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ApplySearchFilter();
        }


        // Methods
        //

        private void ApplySearchFilter()
        {
            var customFilter = new Predicate<object>(item => (((Customer)item).FirstName != null && ((Customer)item).FirstName.StartsWith(tbxSearch.Text, StringComparison.CurrentCultureIgnoreCase))
                                                                || ((Customer)item).LastName != null && ((Customer)item).LastName.StartsWith(tbxSearch.Text, StringComparison.CurrentCultureIgnoreCase)
                                                                || ((Customer)item).Email != null && ((Customer)item).Email.StartsWith(tbxSearch.Text, StringComparison.CurrentCultureIgnoreCase)
                                                                || ((Customer)item).RegNumber.ToString() == tbxSearch.Text);
            dgCustomers.Items.Filter = customFilter;
        }

        private void SetEditCustomerBindings()
        {
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

        private void SwitchSubView(SubView subView)
        {
            switch (subView)
            {
                default:
                case SubView.Customers:
                    gridCustomers.Visibility = Visibility.Visible;
                    gridEditCustomer.Visibility = Visibility.Collapsed;
                    break;

                case SubView.Add:
                    tbEditTitle.Text = "Create Customer";
                    gridCustomers.Visibility = Visibility.Collapsed;
                    gridEditCustomer.Visibility = Visibility.Visible;
                    break;
                case SubView.Edit:
                    tbEditTitle.Text = "Edit Customer";
                    gridCustomers.Visibility = Visibility.Collapsed;
                    gridEditCustomer.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}
