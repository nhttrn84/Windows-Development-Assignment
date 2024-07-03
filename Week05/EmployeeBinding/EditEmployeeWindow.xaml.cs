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
using Microsoft.Data.SqlClient;

namespace EmployeeBinding
{
    /// <summary>
    /// Interaction logic for EditEmployeeWindow.xaml
    /// </summary>
    public partial class EditEmployeeWindow : Window
    {
        Employee oldEmployee;
        public EditEmployeeWindow(Employee employee)
        {
            InitializeComponent();
            oldEmployee = employee;
            fullNameTextBox.Text = employee.FullName;
            emailTextBox.Text = employee.Email;
            addressTextBox.Text = employee.Address;
            phoneNumberTextBox.Text = employee.TelephoneNumber;
            avatarPathTextBox.Text = employee.AvatarPath;
        }

        private void editEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            string fullname = fullNameTextBox.Text;
            string email = emailTextBox.Text;
            string address = addressTextBox.Text;
            string phoneNumber = phoneNumberTextBox.Text;
            string avatarPath = avatarPathTextBox.Text;

            string sql = @"
                UPDATE Employee
                SET Full_Name = @FullName, 
                    Email = @Email, 
                    Address = @Address, 
                    Phone = @Phone, 
                    Avatar = @Avatar
                WHERE Full_Name = @EmployeeFullName
                    AND Email = @EmployeeEmail
                    AND Address = @EmployeeAddress
                    AND Phone = @EmployeePhone
                    AND Avatar = @EmployeeAvatar
            ";

            using (SqlCommand command = new SqlCommand(sql, DB.Instance.Connection))
            {
                command.Parameters.AddWithValue("@FullName", fullname);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Address", address);
                command.Parameters.AddWithValue("@Phone", phoneNumber);
                command.Parameters.AddWithValue("@Avatar", avatarPath);

                command.Parameters.AddWithValue("@EmployeeFullName", oldEmployee.FullName);
                command.Parameters.AddWithValue("@EmployeeEmail", oldEmployee.Email);
                command.Parameters.AddWithValue("@EmployeeAddress", oldEmployee.Address);
                command.Parameters.AddWithValue("@EmployeePhone", oldEmployee.TelephoneNumber);
                command.Parameters.AddWithValue("@EmployeeAvatar", oldEmployee.AvatarPath);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show($"Edit successfully");
                }
                else
                {
                    MessageBox.Show("Edit failed");
                }
            }

            var screen = new MainWindow();
            screen.Show();
            this.Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new MainWindow();
            screen.Show();
            this.Close();
        }
    }
}
