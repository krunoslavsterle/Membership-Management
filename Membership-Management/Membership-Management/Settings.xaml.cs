using Membership_Management.Infrastructure.Models;
using Membership_Management.Services;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Membership_Management
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        private ImportExportService importExportService = new ImportExportService();
        private DatabaseService databaseService = new DatabaseService();

        public Settings()
        {
            InitializeComponent();

            var smtpSettings = databaseService.GetSMTPSettings();
            var masterPassword = databaseService.GetMasterPassword();

            tbxPortNumber.Text = smtpSettings.Port.ToString();
            tbxServerName.Text = smtpSettings.ServerName;
            tbxUsername.Text = smtpSettings.Username;
            tbxPassword.Password = smtpSettings.Password;
            tbxMasterPassword.Password = masterPassword.Password;
            tbDatabasePath.Text = databaseService.DatabasePath;
        }

        private async void BtnImportJSON_Click(object sender, RoutedEventArgs e)
        {
            var filePath = tbxFilePath.Text;
            if (!File.Exists(filePath))
            {
                MessageBox.Show("File not exist!", "Import/Export Error");
                return;
            }

            try
            {
                importExportService.ImportJSON(filePath);
                MessageBox.Show("File imported!", "Import/Export Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Issue while importing File: {ex.Message}", "Import/Export Error");
            }
        }

        private async void BtnImportJSONLegacy_Click(object sender, RoutedEventArgs e)
        {
            var filePath = tbxFilePath.Text;
            if (!File.Exists(filePath))
            {
                MessageBox.Show("File not exist!", "Import/Export Error");
                return;
            }

            try
            {
                importExportService.ImportOldJSON(filePath);
                MessageBox.Show("File imported!", "Import/Export Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Issue while importing File: {ex.Message}", "Import/Export Error");
            }
        }

        private async void BtnExportJSON_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                importExportService.ExportJSON(filePath);
                MessageBox.Show("File exported to Destkop", "Import/Export Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Issue while exporting File: {ex.Message}", "Import/Export Error");
            }
        }

        private void BtnSaveSMTP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var smtpSettings = new SMTPSettings
                {
                    Id = 1,
                    Password = tbxPassword.Password,
                    Port = Int32.Parse(tbxPortNumber.Text),
                    ServerName = tbxServerName.Text,
                    Username = tbxUsername.Text
                };

                databaseService.UpdateSMTPSettings(smtpSettings);
                MessageBox.Show("SMTP Settings Updated", "SMTP Settings Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Issue while updating SMTP Settings: {ex.Message}", "SMTP Settings Error");
            }
        }

        private void BtnUpdateMasterPassword_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbxMasterPassword.Password))
            {
                MessageBox.Show("Master Password is mandatory!", "Master Password Error");
                return;
            }

            try
            {
                var masterPassword = new MasterPassword
                {
                    Id = 1,
                    Password = tbxMasterPassword.Password                   
                };

                databaseService.UpdateMasterPassword(masterPassword);
                MessageBox.Show("Master Password Updated", "Master Password Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Issue while updating Master Password: {ex.Message}", "Master Password Error");
            }
        }
    }
}
