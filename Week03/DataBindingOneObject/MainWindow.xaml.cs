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

namespace DataBindingOneObject
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

        private void DatabindingBook_Button_Click(object sender, RoutedEventArgs e)
        {
            var screen = new BookDatabinding();
            screen.Show();

            this.Deactivated += (s, ev) =>
            {
                screen.Close();
                this.Deactivated -= (s2, ev2) => { };
            };
        }

        private void DatabindingEmployee_Button_Click(object sender, RoutedEventArgs e)
        {
            var screen = new EmployeeDatabinding();
            screen.Show();

            this.Deactivated += (s, ev) =>
            {
                screen.Close();
                this.Deactivated -= (s2, ev2) => { };
            };
        }

        private void DatabindingPhone_Button_Click(object sender, RoutedEventArgs e)
        {
            var screen = new MobilePhoneDatabinding();
            screen.Show();

            this.Deactivated += (s, ev) =>
            {
                screen.Close();
                this.Deactivated -= (s2, ev2) => { };
            };
        }
    }
}
