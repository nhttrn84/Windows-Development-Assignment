using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBindingList
{
    public class Employee : INotifyPropertyChanged
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }
        public string AvatarPath { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
