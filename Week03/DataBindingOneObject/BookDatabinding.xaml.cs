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
    /// Interaction logic for BookDatabinding.xaml
    /// </summary>
    public partial class BookDatabinding : Window
    {
        public BookDatabinding()
        {
            InitializeComponent();
        }

        Book _book;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _book = new Book()
            {
                Name = "Harry Potter and the Philosopher's Stone",
                Author = "J.K.Rowling",
                PublishedYear = "1997",
                ImagePath = "assets/harrypotter.jpg"
            };

            DataContext = _book;
        }
    }
}
