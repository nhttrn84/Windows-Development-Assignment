using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace DataBindingList
{
    /// <summary>
    /// Interaction logic for MobilePhoneWindow.xaml
    /// </summary>
    public partial class MobilePhoneWindow : Window
    {
        public MobilePhoneWindow()
        {
            InitializeComponent();
        }

        BindingList<MobilePhone> _mobilePhones;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _mobilePhones = new BindingList<MobilePhone>()
            {
                new MobilePhone()
                {
                    Name = "Iphone 14 Pro Max",
                    ImagePath = "assets/iphone14.jpg",
                    Manufacturer = "Apple",
                    Price = "27.190.000 ₫"
                },
                new MobilePhone()
                {
                    Name = "Google Pixel 8 Pro",
                    ImagePath = "assets/googlepixel8pro.jpg",
                    Manufacturer = "Google",
                    Price = "20.590.000 ₫"
                },
                new MobilePhone()
                {
                    Name = "Huawei Mate X3",
                    ImagePath = "assets/huaweimatex3.jpg",
                    Manufacturer = "Huawei",
                    Price = "40.220.000 ₫"
                },
                new MobilePhone()
                {
                    Name = "LG W41 Pro",
                    ImagePath = "assets/lgw41pro.jpg",
                    Manufacturer = "LG",
                    Price = "4.300.000 ₫"
                },
                new MobilePhone()
                {
                    Name = "Motorola Razr 2020",
                    ImagePath = "assets/motorolarazr.jpg",
                    Manufacturer = "Motorola",
                    Price = "32.090.000 ₫"
                },
                new MobilePhone()
                {
                    Name = "Nokia X30",
                    ImagePath = "assets/nokiax30.jpg",
                    Manufacturer = "Nokia",
                    Price = "12.590.000 ₫"
                },
                new MobilePhone()
                {
                    Name = "One Plus 12",
                    ImagePath = "assets/oneplus12.jpg",
                    Manufacturer = "One Plus",
                    Price = "14.950.000 ₫"
                },
                new MobilePhone()
                {
                    Name = "Samsung Galaxy A25",
                    ImagePath = "assets/samsunggalaxya25.jpg",
                    Manufacturer = "Samsung",
                    Price = "6.590.000 ₫"
                },
                new MobilePhone()
                {
                    Name = "Sony Xperia 5V",
                    ImagePath = "assets/sonyxperia5v.jpg",
                    Manufacturer = "Sony",
                    Price = "25.980.000 ₫"
                },
                new MobilePhone()
                {
                    Name = "Xiaomi 14 Pro",
                    ImagePath = "assets/xiaomi14pro.jpg",
                    Manufacturer = "Xiao Mi",
                    Price = "16.950.000 ₫"
                }
            };

            mobilePhonesComboBox.ItemsSource = _mobilePhones;
        }

        private void AddMobilePhoneButton_Click(object sender, RoutedEventArgs e)
        {
            _mobilePhones.Add(new MobilePhone()
            {
                Name = "Samsung Galaxy A25",
                ImagePath = "assets/samsunggalaxya25.jpg",
                Manufacturer = "Samsung",
                Price = "6.590.000 ₫"
            });
            MessageBox.Show("Added");
        }

        private void DeleteMobilePhoneButton_Click(object sender, RoutedEventArgs e)
        {
            if (mobilePhonesComboBox.SelectedIndex >= 0)
            {
                int i = mobilePhonesComboBox.SelectedIndex;
                _mobilePhones.RemoveAt(i);
                MessageBox.Show("Deleted");
            }
        }

        private void EditMobilePhoneButton_Click(object sender, RoutedEventArgs e)
        {
            if (mobilePhonesComboBox.SelectedIndex >= 0)
            {
                int i = mobilePhonesComboBox.SelectedIndex;
                _mobilePhones[i].Name = "Iphone 14 Pro Max";
                _mobilePhones[i].ImagePath = "assets/iphone14.jpg";
                MessageBox.Show("Edited");
            }
        }
    }
}
