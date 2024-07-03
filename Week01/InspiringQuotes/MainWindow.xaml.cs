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

namespace InspiringQuotes
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

            Dictionary<string, string> motivationQuotes = new Dictionary<string, string>{
                { "Images/1.png", "Tough times don’t last. Tough people do." },
                { "Images/2.png", "Be so good they can’t ignore you." },
                { "Images/3.png", "Look in the mirror. That’s your competition." },
                { "Images/4.png", "You have to be at your strongest when you’re feeling at your weakest." },
                { "Images/5.png", "Don’t wait for opportunity. Create it." },
                { "Images/6.png", "The first and greatest victory is to conquer self." },
                { "Images/7.png", "The pain you feel today will be the strength you feel tomorrow." },
                { "Images/8.png", "You don’t want to look back and know you could have done better." },
                { "Images/9.png", "Never give up. Great things take time. Be patient." },
                { "Images/10.png", "With confidence you have won before you have started." },
                { "Images/11.png", "A problem is a chance for you to do your best." },
                { "Images/12.png", "Motivation is what gets you started. Habit is what keeps you going." },
                { "Images/13.png", "A little progress each day adds up to big results." },
                { "Images/14.png", "If you get tired, learn to rest, not quit." },
                { "Images/15.png", "Success consists of getting up just one more time than you fall." }
            };

            int i = rng.Next(motivationQuotes.Count);
            string quote = motivationQuotes.ElementAt(i).Value;
            quoteLabel.Content = quote;

            string picture = motivationQuotes.ElementAt(i).Key;

            var bitmap = new BitmapImage(
                new Uri(picture, UriKind.Relative)
            ); // Unique resource identifier
            quoteImage.Source = bitmap;
        }
    }
}
