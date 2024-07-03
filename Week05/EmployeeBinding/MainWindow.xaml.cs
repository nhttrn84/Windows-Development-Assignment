using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        }

        BindingList<Employee> _employees;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAllEmployees();
        }

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
                var sql = "select * from employee";
                var command = new SqlCommand(sql, DB.Instance.Connection);
                
                var reader = command.ExecuteReader();

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
                    }

                }
                reader.Close();
                connection.Close();

                employeesComboBox.ItemsSource = _employees;
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
    }
}
