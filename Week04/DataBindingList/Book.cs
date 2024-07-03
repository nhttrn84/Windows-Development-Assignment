using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataBindingList
{
    public class Book : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Author { get; set; }
        public string PublishedYear { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
