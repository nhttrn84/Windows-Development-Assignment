using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBindingOneObject
{
    public class MobilePhone : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string ImagePath {  get; set; }
        public string Manufacturer { get; set; }
        public string Price { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
