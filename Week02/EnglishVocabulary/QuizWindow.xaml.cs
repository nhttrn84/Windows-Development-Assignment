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

namespace EnglishVocabulary
{
    /// <summary>
    /// Interaction logic for QuizWindow.xaml
    /// </summary>
    public partial class QuizWindow : Window
    {
        public QuizWindow()
        {
            InitializeComponent();
            DisplayRandomQuiz();
        }

        QuizScore _q = new QuizScore();
        private void optionButton_Click(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;
            if (button != null)
            {
                bool? tagValue = button.Tag as bool?;
                if (tagValue.HasValue && tagValue.Value == true)
                {
                    _q._score++;
                }
                DisplayRandomQuiz();
            }
        }

        private void DisplayRandomQuiz()
        {
            if (_q._quizNumber < 10)
            {
                _q._quizNumber++;
                
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
                int j = rng.Next(englishVocabulary.Count);
                string englishWord = englishVocabulary.ElementAt(i).Value;
                string falseEnglishWord = englishVocabulary.ElementAt((i + j) % englishVocabulary.Count).Value;

                int opt = rng.Next(2);

                if (opt == 0)
                {
                    option1Button.Content = englishWord;
                    option1Button.Tag = true;
                    option2Button.Content = falseEnglishWord;
                    option2Button.Tag = false;
                }
                else
                {
                    option1Button.Content = falseEnglishWord;                    
                    option1Button.Tag = false;
                    option2Button.Content = englishWord;
                    option2Button.Tag = true;
                }

                // C:\Windows\Dev\ -> tan cung co san dau \
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string picture = $"{baseDirectory}{englishVocabulary.ElementAt(i).Key}";

                var bitmap = new BitmapImage(
                    new Uri(picture, UriKind.Absolute)
                ); // Unique resource identifier
                quizImage.Source = bitmap;
            }
            else
            {
                MessageBox.Show($"Tổng số điểm đạt được là: {_q._score}"); 
                var screen = new MainWindow();
                screen.Show();

                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DisplayRandomQuiz();
        }
    }
}
