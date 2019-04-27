using Membership_Management.Services;
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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        private ImportExportService importExportService = new ImportExportService();

        public Settings()
        {
            InitializeComponent();
        }

        private void BtnImportOldJSON_Click(object sender, RoutedEventArgs e)
        {
            var filePath = tbxFilePath.Text;
            importExportService.ImportOldJSON(filePath);
        }
    }
}
