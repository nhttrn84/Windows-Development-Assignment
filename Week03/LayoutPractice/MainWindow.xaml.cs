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

namespace LayoutPractice
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

        private void Layout01_Button_Click(object sender, RoutedEventArgs e)
        {
            var screen = new Layout01();
            screen.Show();

            this.Deactivated += (s, ev) =>
            {
                screen.Close();
                this.Deactivated -= (s2, ev2) => { };
            };
        }

        private void Layout02_Button_Click(object sender, RoutedEventArgs e)
        {
            var screen = new Layout02();
            screen.Show();

            this.Deactivated += (s, ev) =>
            {
                screen.Close();
                this.Deactivated -= (s2, ev2) => { };
            };
        }

        private void Layout03_Button_Click(object sender, RoutedEventArgs e)
        {
            var screen = new Layout03();
            screen.Show();

            this.Deactivated += (s, ev) =>
            {
                screen.Close();
                this.Deactivated -= (s2, ev2) => { };
            };
        }
    }
}
