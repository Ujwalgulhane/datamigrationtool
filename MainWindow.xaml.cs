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
using System.IO;
using System.Threading.Tasks;
using mydatamigration.includes;
using mydatamigration.dashboard;

namespace mydatamigration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool IsFirstTimeInstallation()
        {
            string configFilePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "config.json"); // Make sure path is correct
            return !File.Exists(configFilePath);  // If the file doesn't exist, it's the first time
        }


        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Show the preloader overlay
            PreloaderOverlay.Visibility = Visibility.Visible;

            try
            {
                // Simulate login processing (e.g., call API, validate credentials)
                await Task.Run(() =>
                {
                    // Simulating delay for login process (replace this with actual login logic)
                    System.Threading.Thread.Sleep(3000);
                });

                // Simulating user input for username and password
                string username = UsernameTextBox.Text; // Assuming you have a TextBox named UsernameTextBox
                string password = PasswordBox.Password; // Assuming you have a PasswordBox named PasswordBox

                // Validate credentials
                if (ValidateCredentials(username, password))
                {
                    // Check if it's the first time installation
                    if (IsFirstTimeInstallation())
                    {
                        // Show the Database Connection Window
                        DatabaseConnectionWindow dbWindow = new DatabaseConnectionWindow();
                        dbWindow.Show(); // Open the Database Connection Window
                        this.Close();    // Optionally, close the current window
                    }
                    else
                    {
                        // Show the Dashboard
                        DashboardWindow dashboard = new DashboardWindow();
                        dashboard.Show(); // Open the Dashboard Window
                        this.Close();     // Close the Login Window
                    }
                }
                else
                {
                    // Invalid credentials
                    MessageBox.Show("Invalid username or password.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                // Hide the preloader overlay
                PreloaderOverlay.Visibility = Visibility.Collapsed;
            }
        }

        private bool ValidateCredentials(string username, string password)
        {
            // Replace with actual validation logic (e.g., call to a database or API)
            return username == "admin" && password == "admin";
        }

    }

}
