using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
using Microsoft.Data.SqlClient;

namespace EmployeeBinding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<int> itemNumbers = [5, 10, 15, 20];
            itemNumberComboBox.ItemsSource = itemNumbers;
            itemNumberComboBox.SelectedIndex = 1;

            List<string> sortedValues = ["Full_Name", "Email", "Phone", "Address"];
            sortComboBox.ItemsSource = sortedValues;
            sortComboBox.SelectedIndex = 0;
        }

        BindingList<Employee> _employees;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAllEmployees();
        }

        int _rowsPerPage = 10;
        int _totalPages = -1;
        int _totalItems = -1;
        int _currentPage = 1;
        string _sorted = "Full_Name";
        string _minPhoneValue = "0000000000";
        string _maxPhoneValue = "9999999999";
        private async void LoadAllEmployees()
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = ".\\SQLEXPRESS";
            builder.InitialCatalog = "Employee";
            builder.TrustServerCertificate = true;
            builder.IntegratedSecurity = true;

            string connectionString = builder.ConnectionString;

            var connection = new SqlConnection(connectionString);

            connection = await Task.Run(() => {
                var _connection = new SqlConnection(connectionString);

                try
                {
                    _connection.Open();
                    DB.Instance.ConnectionString = connectionString;
                }
                catch (Exception ex)
                {
                    _connection = null; 
                    MessageBox.Show($"Connection Error: {ex.Message}");
                }

                return _connection;
            });

            if (connection != null)
            {
                var sql = $@"
                    SELECT *, COUNT(*) OVER() AS Total
                    FROM Employee
                    WHERE Full_Name LIKE @Keyword
                        AND Phone >= @MinPhone
                        AND Phone <= @MaxPhone
                    ORDER BY {_sorted}
                    OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY
                ";
                var command = new SqlCommand(sql, DB.Instance.Connection);
                command.Parameters.Add("@Skip", SqlDbType.Int)
                .Value = (_currentPage - 1) * _rowsPerPage;
                command.Parameters.Add("@Take", SqlDbType.Int)
                    .Value = _rowsPerPage;

                var keyword = keywordTextBox.Text;
                command.Parameters.Add("@Keyword", SqlDbType.Text)
                    .Value = $"%{keyword}%";

                command.Parameters.AddWithValue("@MinPhone", _minPhoneValue);
                command.Parameters.AddWithValue("@MaxPhone", _maxPhoneValue);

                var reader = command.ExecuteReader();

                int count = -1;
                _employees = new BindingList<Employee>();
                
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        string full_name = (string)reader["Full_Name"];
                        string email = (string)reader["Email"];
                        string address = (string)reader["Address"];
                        string phone = (string)reader["Phone"];
                        string avatar = (string)reader["Avatar"];

                        var employee = new Employee()
                        {
                            FullName = full_name,
                            Email = email,
                            Address = address,
                            TelephoneNumber = phone,
                            AvatarPath = avatar,
                        };

                        _employees.Add(employee);
                        count = (int)reader["Total"];
                    }

                }
                reader.Close();
                connection.Close();

                employeesComboBox.ItemsSource = _employees;

                if (count != _totalItems)
                {
                    _totalItems = count;
                    _totalPages = (_totalItems / _rowsPerPage) +
                        (((_totalItems % _rowsPerPage) == 0) ? 0 : 1);

                    // Tạo thông tin phân trang cho combobox
                    var pageInfos = new List<object>();

                    for (int i = 1; i <= _totalPages; i++)
                    {
                        pageInfos.Add(new
                        {
                            Page = i,
                            Total = _totalPages
                        });
                    };

                    pagingComboBox.ItemsSource = pageInfos;
                    pagingComboBox.SelectedIndex = 0;
                }

                Title = $"Displaying {_employees.Count} / {_totalItems}";
            }

        }

        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new AddEmployeeWindow();
            screen.Show();
            this.Close();
        }

        private void DeleteEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (employeesComboBox.SelectedIndex >= 0)
            {
                Employee selectedEmployee = _employees[employeesComboBox.SelectedIndex];

                string sql = @"DELETE FROM Employee
                               WHERE Full_Name = @FullName
                                  AND Email = @Email
                                  AND Address = @Address
                                  AND Phone = @Phone
                                  AND Avatar = @Avatar";

                using (SqlCommand command = new SqlCommand(sql, DB.Instance.Connection))
                {
                    command.Parameters.AddWithValue("@FullName", selectedEmployee.FullName);
                    command.Parameters.AddWithValue("@Email", selectedEmployee.Email);
                    command.Parameters.AddWithValue("@Address", selectedEmployee.Address);
                    command.Parameters.AddWithValue("@Phone", selectedEmployee.TelephoneNumber);
                    command.Parameters.AddWithValue("@Avatar", selectedEmployee.AvatarPath);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        _employees.RemoveAt(employeesComboBox.SelectedIndex);
                        MessageBox.Show("Deleted");
                    }
                    else
                    {
                        MessageBox.Show("No matching record found for deletion");
                    }
                }
            }
        }

        private void EditEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (employeesComboBox.SelectedIndex >= 0)
            {
                Employee selectedEmployee = _employees[employeesComboBox.SelectedIndex];
                var screen = new EditEmployeeWindow(selectedEmployee);
                screen.Show();
                this.Close();
            }
        }

        private void pagingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic info = pagingComboBox.SelectedItem;
            if (info != null)
            {
                if (info?.Page != _currentPage)
                {
                    _currentPage = info?.Page;
                    LoadAllEmployees();
                }
            }
        }

        private void itemNumberComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic page = itemNumberComboBox.SelectedItem;
            if (page != null)
            {
                if (page != _rowsPerPage)
                {
                    _rowsPerPage = page;
                    _currentPage = 1;
                    _totalPages = (_totalItems / _rowsPerPage) +
                        (((_totalItems % _rowsPerPage) == 0) ? 0 : 1);

                    // Tạo thông tin phân trang cho combobox
                    var pageInfos = new List<object>();

                    for (int i = 1; i <= _totalPages; i++)
                    {
                        pageInfos.Add(new
                        {
                            Page = i,
                            Total = _totalPages
                        });
                    };

                    pagingComboBox.ItemsSource = pageInfos;
                    pagingComboBox.SelectedIndex = 0;
                    LoadAllEmployees();
                }
            }
        }

        private void sortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic sorted = sortComboBox.SelectedItem;
            if (sorted != null)
            {
                if (sorted != _sorted)
                {
                    _sorted = sorted;
                    LoadAllEmployees();
                }
            }
        }

        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            if (pagingComboBox.SelectedIndex > 0)
            {
                pagingComboBox.SelectedIndex--;
            }
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if (pagingComboBox.SelectedIndex < _totalPages)
            {
                pagingComboBox.SelectedIndex++;
            }
        }
        private void keywordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _minPhoneValue = fromTextBox.Text;
                _maxPhoneValue = toTextBox.Text;
                LoadAllEmployees();
            }
        }
    }
}
