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
                { "Images/15.png", "Whale" }
            };

            int i = rng.Next(englishVocabulary.Count);
            string englishWord = englishVocabulary.ElementAt(i).Value;
            wordLabel.Content = englishWord;

            string picture = englishVocabulary.ElementAt(i).Key;

            var bitmap = new BitmapImage(
                new Uri(picture, UriKind.Relative)
            ); // Unique resource identifier
            wordImage.Source = bitmap;
        }
    }
}
