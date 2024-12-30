using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using Newtonsoft.Json;

namespace mydatamigration.includes
{
    public partial class DatabaseConnectionWindow : Window
    {
        private SqlConnection connection;

        public DatabaseConnectionWindow()
        {
            InitializeComponent();
        }

        // Method to test the database connection
        private void Test_Click(object sender, RoutedEventArgs e)
        {
            string serverName = ServerTextBox.Text;
            string username = DbUsernameTextBox.Text;
            string password = DbPasswordBox.Password; // Use .Password for PasswordBox

            // Connection string
            string connectionString = $"Server={serverName};User Id={username};Password={password};";

            // Test the connection
            try
            {
                // Initialize the connection object
                connection = new SqlConnection(connectionString);
                connection.Open(); // Try to open the connection

                MessageBox.Show("Connection successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Enable the ComboBox for database selection after a successful connection
                DatabaseComboBox.IsEnabled = true;

                // Fetch databases after a successful connection
                FetchDatabases();

                // After successful connection, create the config file
                SaveConfigurationToFile(serverName, username, password);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection failed: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Method to fetch all databases and populate the ComboBox
        private void FetchDatabases()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                string query = "SELECT name FROM sys.databases WHERE name NOT IN ('master', 'tempdb', 'model', 'msdb');";

                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DatabaseComboBox.Items.Clear(); // Clear existing items in ComboBox

                            while (reader.Read())
                            {
                                DatabaseComboBox.Items.Add(reader.GetString(0)); // Add database names to ComboBox
                            }

                            if (DatabaseComboBox.Items.Count > 0)
                            {
                                DatabaseComboBox.SelectedIndex = 0; // Select the first database by default
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error fetching databases: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Method to save the configuration to a JSON file
        private void SaveConfigurationToFile(string serverName, string username, string password)
        {
            string configFilePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "config.json");

            // Check if the configuration file already exists
            if (File.Exists(configFilePath))
            {
                return; // If the file already exists, no need to create a new one
            }

            // Create an object to hold the configuration data
            var configData = new
            {
                ServerName = serverName,
                Username = username,
                Password = password,
                DatabaseName = DatabaseComboBox.SelectedItem.ToString() // Save the selected database
            };

            // Serialize the configuration object to JSON
            string json = JsonConvert.SerializeObject(configData, Formatting.Indented);

            // Write the JSON data to the file
            File.WriteAllText(configFilePath, json);
        }

        // Method to save the selected database configuration
        private void SaveConfiguration_Click(object sender, RoutedEventArgs e)
        {
            if (DatabaseComboBox.SelectedItem != null)
            {
                string selectedDatabase = DatabaseComboBox.SelectedItem.ToString();
                MessageBox.Show($"Configuration saved successfully for the database: {selectedDatabase}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                DashboardWindow dashboard = new DashboardWindow();
                dashboard.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select a database before saving the configuration.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Method to check if it's the first time installation (if config.json doesn't exist)
        private bool IsFirstTimeInstallation()
        {
            string configFilePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "config.json");
            return !File.Exists(configFilePath);  // If the file doesn't exist, it's the first time
        }

        // Load the config data (optional, for reading configuration)
        private void LoadConfiguration()
        {
            string configFilePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "config.json");
            if (File.Exists(configFilePath))
            {
                string json = File.ReadAllText(configFilePath);
                var configData = JsonConvert.DeserializeObject<dynamic>(json);

                string serverName = configData.ServerName;
                string username = configData.Username;
                string password = configData.Password;
                string databaseName = configData.DatabaseName;

                // Use this data to pre-populate the UI or establish a connection on startup
            }
        }
    }
}
