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

namespace EnglishVocabulary
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

        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayRandomData(); // Refactoring
        }

        private void DisplayRandomData()
        {
            Random rng = new Random();

            Dictionary<string, string> englishVocabulary = new Dictionary<string, string>{
                { "Images/1.png", "Cougar" },
                { "Images/2.png", "Polar Bear" },
                { "Images/3.png", "Snow Leopard" },
                { "Images/4.png", "Hyena" },
                { "Images/5.png", "Lion" },
                { "Images/6.png", "Tiger" },
                { "Images/7.png", "Wolf" },
                { "Images/8.png", "Orca" },
                { "Images/9.png", "Grizzly Bear" },
                { "Images/10.png", "Crocodile" },
                { "Images/11.png", "Eagle" },
                { "Images/12.png", "Shark" },
                { "Images/13.png", "Otter" },
                { "Images/14.png", "Cobra" },
                { "Images/15.png", "Whale" },
                { "Images/16.png", "Fox" },
                { "Images/17.png", "Rhino" },
                { "Images/18.png", "Panda" },
                { "Images/19.png", "Peacock" },
                { "Images/20.png", "Water Buffalo" }
            };

            int i = rng.Next(englishVocabulary.Count);
            string englishWord = englishVocabulary.ElementAt(i).Value;
            wordLabel.Content = englishWord;

            // C:\Windows\Dev\ -> tan cung co san dau \
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string picture = $"{baseDirectory}{englishVocabulary.ElementAt(i).Key}";

            var bitmap = new BitmapImage(
                new Uri(picture, UriKind.Absolute)
            ); // Unique resource identifier
            wordImage.Source = bitmap;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DisplayRandomData();
        }

        private void quizButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new QuizWindow();
            screen.Show();

            this.Close();
        }
    }
}
