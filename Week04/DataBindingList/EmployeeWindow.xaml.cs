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
    /// Interaction logic for EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        public EmployeeWindow()
        {
            InitializeComponent();
        }

        BindingList<Employee> _employees;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _employees = new BindingList<Employee>()
            { 
                
                new Employee()
                {
                    FullName = "Biện Diệu Loan",
                    AvatarPath = "assets/avatar01.jpg",
                    Address = "Phường 12, Thành phố Đà Lạt, Lâm Đồng",
                    Email = "biendieuloan214@google.com",
                    TelephoneNumber = "086 823 1970"
                },
                new Employee()
                {
                    FullName = "Điều Ngọc Liên",
                    AvatarPath = "assets/avatar02.jpg",
                    Address = "Xã Vân Khánh Tây, Huyện An Minh, Kiên Giang",
                    Email = "dieungoclien397@icloud.com",
                    TelephoneNumber = "056 521 6307"
                },
                new Employee()
                {
                    FullName = "Thành Tịnh Nhi",
                    AvatarPath = "assets/avatar03.jpg",
                    Address = "Phường Phú Lợi, Thành phố Thủ Dầu Một, Bình Dương",
                    Email = "thanhtinhnhi697@hotmail.com",
                    TelephoneNumber = "082 205 4861"
                },
                new Employee()
                {
                    FullName = "Đặng Kim Xuân",
                    AvatarPath = "assets/avatar04.jpg",
                    Address = "Thị trấn Khánh Hải, Huyện Ninh Hải, Ninh Thuận",
                    Email = "dangkimxuan119@yahoo.com",
                    TelephoneNumber = "082 314 2685"
                },
                new Employee()
                {
                    FullName = "Ân Thái Sang",
                    AvatarPath = "assets/avatar05.jpg",
                    Address = "Phường Quảng Phúc, Thị xã Ba Đồn, Quảng Bình",
                    Email = "anthaisang201@icloud.com",
                    TelephoneNumber = "058 402 5381"
                },
                new Employee()
                {
                    FullName = "Cổ Thiện Ðức",
                    AvatarPath = "assets/avatar06.jpg",
                    Address = "Xã Phước Vinh, Huyện Châu Thành, Tây Ninh",
                    Email = "cothienuc645@facebook.com",
                    TelephoneNumber = "032 392 1540"
                },
                new Employee()
                {
                    FullName = "Tán Chí Thành",
                    AvatarPath = "assets/avatar07.jpg",
                    Address = "Xã Thanh Hải, Huyện Lục Ngạn, Bắc Giang",
                    Email = "tanchithanh863@yahoo.com",
                    TelephoneNumber = "097 809 6142"
                },
                new Employee()
                {
                    FullName = "Tán Chính Thuận",
                    AvatarPath = "assets/avatar08.jpg",
                    Address = "Xã Nội Duệ, Huyện Tiên Du, Bắc Ninh",
                    Email = "tanchinhthuan649@microsoft.com",
                    TelephoneNumber = "091 085 6423"
                },
                new Employee()
                {
                    FullName = "Thành Hồng Ngọc",
                    AvatarPath = "assets/avatar09.jpg",
                    Address = "Xã Quang Húc, Huyện Tam Nông, Phú Thọ",
                    Email = "thanhhongngoc411@facebook.com",
                    TelephoneNumber = "085 135 7469"
                },
                new Employee()
                {
                    FullName = "Kiều Duy Thắng",
                    AvatarPath = "assets/avatar10.jpg",
                    Address = "Xã Hải Thượng, Huyện Tĩnh Gia, Thanh Hóa",
                    Email = "kieuduythang918@yahoo.com",
                    TelephoneNumber = "039 475 6290"
                }
            };

            employeesComboBox.ItemsSource = _employees;
        }

        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            _employees.Add(new Employee()
            {
                FullName = "Tán Chính Thuận",
                AvatarPath = "assets/avatar08.jpg",
                Address = "Xã Nội Duệ, Huyện Tiên Du, Bắc Ninh",
                Email = "tanchinhthuan649@microsoft.com",
                TelephoneNumber = "091 085 6423"
            });
            MessageBox.Show("Added");
        }

        private void DeleteEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (employeesComboBox.SelectedIndex >= 0)
            {
                int i = employeesComboBox.SelectedIndex;
                _employees.RemoveAt(i);
                MessageBox.Show("Deleted");
            }
        }

        private void EditEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (employeesComboBox.SelectedIndex >= 0)
            {
                int i = employeesComboBox.SelectedIndex;
                _employees[i].FullName = "Trần Đình Nhật";
                _employees[i].AvatarPath = "assets/avatar05.jpg";
                MessageBox.Show("Edited");
            }
        }
    }
}
