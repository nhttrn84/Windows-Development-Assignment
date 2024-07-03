using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Microsoft.Win32;

namespace MediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool mediaPlayerIsPlaying = false;
        private bool mediaPlayerIsPause = false;
        private bool userIsDraggingSlider = false;
        private bool shuffleMode = false;
        private int currentlyPlayingIndex = -1;
        private MediaItem currentItem = null;
        private ObservableCollection<MediaItem> _mediaItemsList;
        private Random random = new Random();
        private List<MediaItem> recentlyPlayedItems = new List<MediaItem>();

        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            _mediaItemsList = new ObservableCollection<MediaItem>();
            LoadRecentlyPlayedList();

            sliVolume.ValueChanged += sliVolume_ValueChanged;
            sliVolume.AddHandler(MouseLeftButtonUpEvent, new MouseButtonEventHandler(sliVolume_MouseLeftButtonUp), true);

            mePlayer.MediaEnded += mePlayer_MediaEnded;
        }

        private void mePlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            mePlayer.Position = TimeSpan.Zero;
            if (shuffleMode)
            {
                PlayShuffleMode();
            }
            else
            {
                PlayNextMedia(null, null);
            }
        }

        private void sliVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var volume = sender as Slider;
            mePlayer.Volume = volume.Value;
        }

        private void sliVolume_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var volume = sender as Slider;
            mePlayer.Volume = volume.Value;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if ((mePlayer.Source != null) && (mePlayer.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            {
                sliProgress.Minimum = 0;
                sliProgress.Maximum = mePlayer.NaturalDuration.TimeSpan.TotalSeconds;
                sliProgress.Value = mePlayer.Position.TotalSeconds;
            }
        }

        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media files (*.mp3;*.mpg;*.mpeg;*.mp4)|*.mp3;*.mpg;*.mpeg;*.mp4|All files (*.*)|*.*";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == true)
            {
                _mediaItemsList.Clear();

                foreach (string fileName in openFileDialog.FileNames)
                {
                    // Create a new MediaItem instance for each file
                    MediaItem mediaItem = new MediaItem
                    {
                        FileName = System.IO.Path.GetFileName(fileName),
                        FilePath = fileName,
                        LastPlayedPosition = TimeSpan.FromHours(0)
                    };

                    // Add the MediaItem to the list and ListBox
                    _mediaItemsList.Add(mediaItem);
                }

                mediaList.ItemsSource = _mediaItemsList;

                // Play the first file in the list
                PlaySelectedMedia();
            }
        }

        private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (mePlayer != null) && (mePlayer.Source != null);
        }

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (mePlayer != null && mePlayer.Source != null)
            {

                mePlayer.Position = _mediaItemsList[mediaList.SelectedIndex].LastPlayedPosition;
                mePlayer.Play();
                mediaPlayerIsPlaying = true;
                mediaPlayerIsPause = false;
            }
        }

        private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (mePlayer.Source != null)
            {
                MediaItem currentMediaItem = _mediaItemsList[currentlyPlayingIndex];
                currentMediaItem.LastPlayedPosition = mePlayer.Position;
                mediaPlayerIsPause = true;
            }

            mePlayer.Pause();
        }

        private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (mePlayer.Source != null)
            {
                MediaItem currentMediaItem = _mediaItemsList[currentlyPlayingIndex];
                currentMediaItem.LastPlayedPosition = mePlayer.Position;
            }

            mePlayer.Stop();
            mediaPlayerIsPlaying = false;
            mediaPlayerIsPause = false;
        }

        private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }

        private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            mePlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            mePlayer.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        }

        private void MediaList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PlaySelectedMedia();
        }

        private void PlaySelectedMedia()
        {
            if (mediaList.SelectedItem != null)
            {
                if (currentItem != null)
                {
                    currentItem.LastPlayedPosition = mePlayer.Position;
                }

                MediaItem selectedMediaItem = _mediaItemsList[mediaList.SelectedIndex];
                currentlyPlayingIndex = mediaList.SelectedIndex;
                currentItem = selectedMediaItem;
                try
                {
                    mePlayer.Source = new Uri(selectedMediaItem.FilePath);
                    mePlayer.Position = selectedMediaItem.LastPlayedPosition;
                    mePlayer.Play();
                    mediaPlayerIsPlaying = true;
                    mediaPlayerIsPause = false;

                    AddToRecentlyPlayed(selectedMediaItem);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error playing media: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void AddToRecentlyPlayed(MediaItem mediaItem)
        {
            MediaItem existingItem = recentlyPlayedItems.FirstOrDefault(item => item.FilePath == mediaItem.FilePath);

            if (existingItem != null)
            {
                existingItem.LastPlayedPosition = mediaItem.LastPlayedPosition;
            }
            else
            {
                recentlyPlayedItems.Add(mediaItem);

                const int maxRecentlyPlayedItems = 10;
                if (recentlyPlayedItems.Count > maxRecentlyPlayedItems)
                {
                    recentlyPlayedItems.RemoveAt(recentlyPlayedItems.Count - 1);
                }
            }

            SaveRecentlyPlayedList();
        }

        private void SaveRecentlyPlayedList()
        {
            string filePath = "recentlyPlayed.json";
            string json = JsonSerializer.Serialize(recentlyPlayedItems.ToList());
            File.WriteAllText(filePath, json);
        }

        private void LoadRecentlyPlayedList()
        {
            string filePath = "recentlyPlayed.json";
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                List<MediaItem> loadedList = JsonSerializer.Deserialize<List<MediaItem>>(json);

                foreach (var loadedItem in loadedList)
                {
                    MediaItem existingItem = recentlyPlayedItems.FirstOrDefault(item => item.FilePath == loadedItem.FilePath);

                    if (existingItem != null)
                    {
                        existingItem.LastPlayedPosition = loadedItem.LastPlayedPosition;
                    }
                    else
                    {
                        recentlyPlayedItems.Add(loadedItem);
                    }
                }

                if (recentlyPlayedItems.Count > 0)
                {
                    _mediaItemsList = new ObservableCollection<MediaItem>(recentlyPlayedItems);
                    mediaList.ItemsSource = _mediaItemsList;
                }
            }
        }

        private void PlayShuffleMode()
        {
            int itemCount = _mediaItemsList.Count;
            int newIndex;

            do
            {
                newIndex = random.Next(itemCount);
            } while (newIndex == currentlyPlayingIndex);

            if (currentItem != null)
            {
                currentItem.LastPlayedPosition = mePlayer.Position;
            }

            currentlyPlayingIndex = newIndex;
            MediaItem mediaItem = _mediaItemsList[currentlyPlayingIndex];
            currentItem = mediaItem;
            mePlayer.Source = new Uri(mediaItem.FilePath);
            mePlayer.Position = mediaItem.LastPlayedPosition;
            mePlayer.Play();
            mediaPlayerIsPlaying = true;
            mediaPlayerIsPause = false;
            mediaList.SelectedIndex = currentlyPlayingIndex;
        }

        private void PlayNextMedia(object sender, RoutedEventArgs e)
        {
            if (shuffleMode)
            {
                PlayShuffleMode();
            }
            else
            {
                if (currentlyPlayingIndex < _mediaItemsList.Count - 1)
                {
                    if (currentItem != null)
                    {
                        currentItem.LastPlayedPosition = mePlayer.Position;
                    }

                    currentlyPlayingIndex++;
                    MediaItem nextMediaItem = _mediaItemsList[currentlyPlayingIndex];
                    currentItem = nextMediaItem;
                    mePlayer.Source = new Uri(nextMediaItem.FilePath);
                    mePlayer.Position = nextMediaItem.LastPlayedPosition;
                    mePlayer.Play();
                    mediaPlayerIsPlaying = true;
                    mediaPlayerIsPause = false;
                    mediaList.SelectedIndex = currentlyPlayingIndex;
                }
            }
        }

        private void PlayPreviousMedia(object sender, RoutedEventArgs e)
        {
            if (shuffleMode)
            {
                PlayShuffleMode();
            }
            else
            {
                if (currentlyPlayingIndex > 0)
                {
                    if (currentItem != null)
                    {
                        currentItem.LastPlayedPosition = mePlayer.Position;
                    }

                    currentlyPlayingIndex--;
                    MediaItem previousMediaItem = _mediaItemsList[currentlyPlayingIndex];
                    currentItem = previousMediaItem;
                    mePlayer.Source = new Uri(previousMediaItem.FilePath);
                    mePlayer.Position = previousMediaItem.LastPlayedPosition;
                    mePlayer.Play();
                    mediaPlayerIsPlaying = true;
                    mediaPlayerIsPause = false;
                    mediaList.SelectedIndex = currentlyPlayingIndex;
                }
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON File (*.json)|*.json|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string filePath = openFileDialog.FileName;
                    string json = File.ReadAllText(filePath);

                    List<MediaItem> loadedList = JsonSerializer.Deserialize<List<MediaItem>>(json);

                    _mediaItemsList.Clear();
                    foreach (var item in loadedList)
                    {
                        _mediaItemsList.Add(item);
                    }

                    mediaList.ItemsSource = _mediaItemsList;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading media list: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON File (*.json)|*.json|All files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    string filePath = saveFileDialog.FileName;

                    string json = JsonSerializer.Serialize(_mediaItemsList);

                    File.WriteAllText(filePath, json);

                    MessageBox.Show("Media list saved successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving media list: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void RemoveItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (mediaList.SelectedItem != null)
            {
                _mediaItemsList.RemoveAt(mediaList.SelectedIndex);
                mePlayer.Stop();
                mePlayer.Source = null;
                mediaPlayerIsPlaying = false;
                mediaPlayerIsPause = false;
                mediaList.ItemsSource = _mediaItemsList;
                MessageBox.Show("Remove 1 item from the playlist");
            }
        }

        private void ShuffleButton_Click(object sender, RoutedEventArgs e)
        {
            shuffleMode = !shuffleMode;
            if (shuffleMode)
            {
                shuffle.Source = new BitmapImage(new Uri("/images/shuffle.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                shuffle.Source = new BitmapImage(new Uri("/images/non_shuffle.png", UriKind.RelativeOrAbsolute));
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                TogglePlayPause();
            }
            else if (e.Key == Key.Right)
            {
                PlayNextMedia(null, null);
            }
            else if (e.Key == Key.Left)
            {
                PlayPreviousMedia(null, null);
            }
        }

        private void TogglePlayPause()
        {
            if (mediaPlayerIsPause)
            {
                Play_Executed(null, null);
            }
            else
            {
                Pause_Executed(null, null);
            }
        }
    }
}
