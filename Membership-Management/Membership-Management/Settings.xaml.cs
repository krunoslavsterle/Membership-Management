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

        public Settings()
        {
            InitializeComponent();
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
    }
}
