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

namespace DataBindingList
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

        private void Books_Button_Click(object sender, RoutedEventArgs e)
        {
            var screen = new BookWindow();
            screen.Show();

            this.Deactivated += (s, ev) =>
            {
                screen.Close();
                this.Deactivated -= (s2, ev2) => { };
            };
        }

        private void MobilePhones_Button_Click(object sender, RoutedEventArgs e)
        {
            var screen = new MobilePhoneWindow();
            screen.Show();

            this.Deactivated += (s, ev) =>
            {
                screen.Close();
                this.Deactivated -= (s2, ev2) => { };
            };
        }

        private void Employees_Button_Click(object sender, RoutedEventArgs e)
        {
            var screen = new EmployeeWindow();
            screen.Show();

            this.Deactivated += (s, ev) =>
            {
                screen.Close();
                this.Deactivated -= (s2, ev2) => { };
            };
        }
    }
}
