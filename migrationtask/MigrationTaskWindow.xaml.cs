using mydatamigration.dashboard;
using mydatamigration.migrationdata;
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
using System.Windows.Shapes;

namespace mydatamigration.migrationtask
{
    /// <summary>
    /// Interaction logic for MigrationTaskWindow.xaml
    /// </summary>
    public partial class MigrationTaskWindow : Window
    {
        public MigrationTaskWindow()
        {
            InitializeComponent();
        }
        private void NavigateToDashboard(object sender, RoutedEventArgs e)
        {
            // Create an instance of DashboardWindow
            var dashboardPage = new DashboardWindow();

            // Open the new DashboardWindow
            dashboardPage.Show();


            this.Close();
        }

        private void NavigateToMigrateData(object sender, RoutedEventArgs e)
        {

            var migrateDataPage = new MigrationDataWindow(); // Corrected missing semicolon

            // Open the new MigrationDataWindow
            migrateDataPage.Show();

            // Close the current window (DashboardWindow)
            this.Close();
        }

        private void NavigateToMigrationTasks(object sender, RoutedEventArgs e)
        {
            // Create an instance of MigrationTaskWindow
            var migrationTaskPage = new MigrationTaskWindow(); // Corrected missing semicolon

            // Open the new MigrationTaskWindow
            migrationTaskPage.Show();

            // Close the current window (DashboardWindow)
            this.Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the application when the Exit button is clicked
            Application.Current.Shutdown();
        }
    }
}
