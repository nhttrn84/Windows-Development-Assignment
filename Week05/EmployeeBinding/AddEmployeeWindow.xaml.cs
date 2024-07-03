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
    /// Interaction logic for AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        public AddEmployeeWindow()
        {
            InitializeComponent();
        }

        private void addEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            string fullname = fullNameTextBox.Text;
            string email = emailTextBox.Text;
            string address = addressTextBox.Text;
            string phoneNumber = phoneNumberTextBox.Text;
            string avatarPath = avatarPathTextBox.Text;

            string sql = @"INSERT INTO Employee (Full_Name, Email, Address, Phone, Avatar)
                            VALUES (@FullName, @Email, @Address, @Phone, @Avatar)";

            using (SqlCommand command = new SqlCommand(sql, DB.Instance.Connection))
            {
                command.Parameters.AddWithValue("@FullName", fullname);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Address", address);
                command.Parameters.AddWithValue("@Phone", phoneNumber);
                command.Parameters.AddWithValue("@Avatar", avatarPath);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show($"Insert successfully new employee!");
                }
                else
                {
                    MessageBox.Show("Insert failed");
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
