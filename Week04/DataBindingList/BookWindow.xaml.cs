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
    /// Interaction logic for BookWindow.xaml
    /// </summary>
    public partial class BookWindow : Window
    {
        public BookWindow()
        {
            InitializeComponent();
        }

        BindingList<Book> _books;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _books = new BindingList<Book>()
            {
                new Book() 
                {
                    Name = "Harry Potter and the Philosopher's Stone", 
                    Author = "J.K.Rowling", 
                    PublishedYear = "1997", 
                    ImagePath = "assets/harrypotter.jpg" 
                },
                new Book() 
                {
                    Name = "The Girl with the Dragon Tattoo",
                    Author = "Stieg Larsson",
                    PublishedYear = "2005",
                    ImagePath = "assets/girlwithdragon.jpg" 
                },
                new Book() 
                {
                    Name = "And Then There Were None",
                    Author = "Agatha Christie",
                    PublishedYear = "1939",
                    ImagePath = "assets/therewerenone.jpg" 
                },
                new Book() 
                {
                    Name = "Angels & Demons",
                    Author = "Dan Brown",
                    PublishedYear = "2000",
                    ImagePath = "assets/angelsdemons.jpg" 
                },
                new Book()
                {
                    Name = "Rebecca",
                    Author = "Daphne du Maurier",
                    PublishedYear = "1938",
                    ImagePath = "assets/rebecca.jpg"
                },
                new Book() 
                {
                    Name = "The Godfather",
                    Author = "Mario Puzo",
                    PublishedYear = "1969",
                    ImagePath = "assets/godfather.jpg" 
                },
                new Book() 
                {
                    Name = "The Lovely Bones",
                    Author = "Alice Sebold",
                    PublishedYear = "2002",
                    ImagePath = "assets/lovelybones.jpg"
                },
                new Book()
                {
                    Name = "Gone Girl",
                    Author = "Gillian Flynn",
                    PublishedYear = "2012",
                    ImagePath = "assets/gonegirl.jpg" 
                },
                new Book()
                {
                    Name = "The Name of the Rose",
                    Author = "Umberto Eco",
                    PublishedYear = "1980",
                    ImagePath = "assets/nameoftherose.jpg" 
                },
                new Book()
                {
                    Name = "Shutter Island",
                    Author = "Dennis Lehane",
                    PublishedYear = "2003",
                    ImagePath = "assets/shutterisland.jpg"
                }
            };

            booksComboBox.ItemsSource = _books;
        }

        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            _books.Add(new Book()
            {
                Name = "Harry Potter and the Philosopher's Stone",
                Author = "J.K.Rowling",
                PublishedYear = "1997",
                ImagePath = "assets/harrypotter.jpg"
            });
            MessageBox.Show("Added");
        }

        private void DeleteBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (booksComboBox.SelectedIndex >= 0)
            {
                int i = booksComboBox.SelectedIndex;
                _books.RemoveAt(i);
                MessageBox.Show("Deleted");
            }
        }

        private void EditBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (booksComboBox.SelectedIndex >= 0)
            {
                int i = booksComboBox.SelectedIndex;
                _books[i].Name = "Harry Potter and the Philosopher's Stone";
                _books[i].ImagePath = "assets/harrypotter.jpg";
                MessageBox.Show("Edited");
            }
        }
    }
}
