using System;
using System.IO;
using System.Windows;
using System.Data.SqlClient;
using Microsoft.Win32;
using OfficeOpenXml;
using System.Linq;
using mydatamigration.dashboard;
using mydatamigration.migrationtask;
using System.Threading.Tasks;
using System.Data;
using Newtonsoft.Json.Linq;

namespace mydatamigration.migrationdata
{
    public partial class MigrationDataWindow : Window
    {
        // Database connection string
        private string _connectionString;

        public MigrationDataWindow()
        {
            InitializeComponent();
            LoadConnectionStringFromConfig();
        }

        private void LoadConnectionStringFromConfig()
        {
            string configFilePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "config.json");

            if (File.Exists(configFilePath))
            {
                try
                {
                    string jsonContent = File.ReadAllText(configFilePath);
                    JObject config = JObject.Parse(jsonContent);

                    // Extract values
                    string serverName = config["ServerName"]?.ToString();
                    string username = config["Username"]?.ToString();
                    string password = config["Password"]?.ToString();
                    string databaseName = config["DatabaseName"]?.ToString();

                    // Generate the connection string
                    _connectionString = $"Server={serverName};Database={databaseName};User Id={username};Password={password};";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error reading or parsing config.json: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("config.json file not found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BrowseFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv|Excel Files (*.xlsx;*.xls)|*.xlsx;*.xls"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                FilePathTextBox.Text = openFileDialog.FileName;
            }
        }

        

        private async void UploadFile(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FilePathTextBox.Text) || !File.Exists(FilePathTextBox.Text))
            {
                MessageBox.Show("Please select a valid file.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            PreloaderOverlay.Visibility = Visibility.Visible;
            await Task.Run(() =>
            {
                // Simulating delay for login process (replace this with actual login logic)
                System.Threading.Thread.Sleep(3000);
            });
            PreloaderOverlay.Visibility = Visibility.Collapsed;

            try
            {
                DataTable validData, invalidData;
                ParseFile(FilePathTextBox.Text, out validData, out invalidData);

                ValidDataGrid.ItemsSource = validData.DefaultView;
                InvalidDataGrid.ItemsSource = invalidData.DefaultView;
                /*
                InsertData(validData);
                MessageBox.Show("Data uploaded successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                */
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

       

        private void ParseFile(string filePath, out DataTable validData, out DataTable invalidData)
        {
            validData = new DataTable();
            invalidData = new DataTable();

            // Add columns to DataTables based on your schema
            foreach (var column in new[] { "ANALYSIS", "ANALYSIS_VERSION", "NAME", "ORDER_NUMBER", "RESULT_TYPE", "UNITS", "MINIMUM", "MAXIMUM", "TRUE_WORD", "FALSE_WORD", "ALLOWED_CHARACTERS", "CALCULATION", "PLACES", "REP_CONTROL", "REPLICATES", "SIG_FIGS_NUMBER", "SIG_FIGS_ROUNDING", "SIG_FIGS_FILTER", "MINIMUM_PQL", "MAXIMUM_PQL", "PQL_CALCULATION", "FORMULA", "MATRIX_NO", "MATRIX_NAME", "COLUMN_NO", "COLUMN_NAME", "ROW_NO", "ROW_NAME", "UNCERTAINTY_TEXT", "ENTITY_CRITERIA", "ENTITY_FULL_ID" })
            {
                validData.Columns.Add(column);
                invalidData.Columns.Add(column);
            }

            if (filePath.EndsWith(".csv"))
            {
                using (var reader = new StreamReader(filePath))
                {
                    string line;
                    bool isFirstRow = true;

                    while ((line = reader.ReadLine()) != null)
                    {
                        // Skip the first row (header)
                        if (isFirstRow)
                        {
                            isFirstRow = false;
                            continue;
                        }

                        var values = line.Split(',');
                        if (values.Length == validData.Columns.Count)
                            validData.Rows.Add(values);
                        else
                            invalidData.Rows.Add(values);
                    }
                }
            }
            else if (filePath.EndsWith(".xlsx") || filePath.EndsWith(".xls"))
            {
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets.First();
                    var rowCount = worksheet.Dimension.Rows;
                    var columnCount = worksheet.Dimension.Columns;

                    for (int row = 2; row <= rowCount; row++) // Start from the second row
                    {
                        var values = new object[columnCount];
                        for (int col = 1; col <= columnCount; col++)
                        {
                            values[col - 1] = worksheet.Cells[row, col].Text;
                        }

                        if (values.Length == validData.Columns.Count)
                            validData.Rows.Add(values);
                        else
                            invalidData.Rows.Add(values);
                    }
                }
            }
        }

        private async void InsertValidData(object sender, RoutedEventArgs e)
        {
            if (ValidDataGrid.ItemsSource == null)
            {
                MessageBox.Show("No valid data to insert.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DataTable validData = ((DataView)ValidDataGrid.ItemsSource).ToTable();

            PreloaderOverlay.Visibility = Visibility.Visible;

            // Simulate delay for 3 seconds
            await Task.Run(() => System.Threading.Thread.Sleep(3000));

            PreloaderOverlay.Visibility = Visibility.Collapsed;

            try
            {
                InsertData(validData);
                MessageBox.Show("Data inserted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void InsertData(DataTable validData)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                foreach (DataRow row in validData.Rows)
                {
                    var command = new SqlCommand("INSERT INTO VERSIONED_COMPONENT VALUES (@ANALYSIS, @ANALYSIS_VERSION, @NAME, @ORDER_NUMBER, @RESULT_TYPE, @UNITS, @MINIMUM, @MAXIMUM, @TRUE_WORD, @FALSE_WORD, @ALLOWED_CHARACTERS, @CALCULATION, @PLACES, @REP_CONTROL, @REPLICATES, @SIG_FIGS_NUMBER, @SIG_FIGS_ROUNDING, @SIG_FIGS_FILTER, @MINIMUM_PQL, @MAXIMUM_PQL, @PQL_CALCULATION, @FORMULA, @MATRIX_NO, @MATRIX_NAME, @COLUMN_NO, @COLUMN_NAME, @ROW_NO, @ROW_NAME, @UNCERTAINTY_TEXT, @ENTITY_CRITERIA, @ENTITY_FULL_ID)", connection);
                    foreach (DataColumn column in validData.Columns)
                    {
                        command.Parameters.AddWithValue($"@{column.ColumnName}", row[column]);
                    }
                    command.ExecuteNonQuery();
                }
            }
        }

        private void NavigateToDashboard(object sender, RoutedEventArgs e)
        {
            var dashboardPage = new DashboardWindow();
            dashboardPage.Show();
            this.Close();
        }

        private void NavigateToMigrateData(object sender, RoutedEventArgs e)
        {
            var migrateDataPage = new MigrationDataWindow();
            migrateDataPage.Show();
            this.Close();
        }

        private void NavigateToMigrationTasks(object sender, RoutedEventArgs e)
        {
            var migrationTaskPage = new MigrationTaskWindow();
            migrationTaskPage.Show();
            this.Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
