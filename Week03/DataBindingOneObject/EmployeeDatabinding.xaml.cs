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

namespace DataBindingOneObject
{
    /// <summary>
    /// Interaction logic for EmployeeDatabinding.xaml
    /// </summary>
    public partial class EmployeeDatabinding : Window
    {
        public EmployeeDatabinding()
        {
            InitializeComponent();
        }

        Employee _employee;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _employee = new Employee
            {
                FullName = "Trần Đình Nhật",
                AvatarPath = "assets/avatar05.jpg",
                Address = "Ngũ Phụng, Phú Quý, Bình Thuận",
                Email = "nhttrn84@gmail.com",
                TelephoneNumber = "0584425210"
            };

            DataContext = _employee;
        }
    }
}
