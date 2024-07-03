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
    /// Interaction logic for MobilePhoneDatabinding.xaml
    /// </summary>
    public partial class MobilePhoneDatabinding : Window
    {
        public MobilePhoneDatabinding()
        {
            InitializeComponent();
        }

        MobilePhone _mobile_phone;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _mobile_phone = new MobilePhone
            {
                Name = "Iphone 14 Pro Max 128GB",
                ImagePath = "assets/iphone14.jpg",
                Manufacturer = "Apple",
                Price = "27.190.000 ₫"
            };

            DataContext = _mobile_phone;
        }
    }
}
