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
using Microsoft.Win32;
using TagLib;
using System.Windows.Threading;


namespace Nasa
{
    /// <summary>
    /// Interaction logic for AudioPlayer.xaml
    /// </summary>
    public partial class AudioPlayer : Window
    {
        private readonly MediaPlayer mediaPlayer = new MediaPlayer();
        private readonly DispatcherTimer timer1 = new DispatcherTimer();
        public string curentFile;
        private bool isPlaying;
        public AudioPlayer()
        {
            InitializeComponent();
            this.titlebox.Text = "Downloading, please wait...";
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void SetTitle(string title)
        {
            this.titlebox.Text = title;
        }
        
        public void SetDuration(double duration)
        {
            ProgressSlider.Maximum = duration;
        }

        public void SetTimer()
        {
            this.timer1.Tick += new EventHandler(UpdateProgress);
            this.timer1.Interval = new TimeSpan(0, 0, 0, 0, 1);
        }
        public void OpenMediafile(string FileUrl)
        {
            mediaPlayer.Open(new Uri(FileUrl));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (isPlaying == false)
            {
                mediaPlayer.Play();
                this.timer1.Start();
                isPlaying = true;
                this.PlayButton.Content = "Pause";
            }
            else
            {
                mediaPlayer.Pause();
                this.timer1.Stop();
                isPlaying = false;
                this.PlayButton.Content = "Play";
            }
        }

        private void UpdateProgress(object sender, EventArgs e)
        {
            ProgressSlider.Value= mediaPlayer.Position.TotalMilliseconds;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mediaPlayer.Stop();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaPlayer.Volume = this.VolumeSlider.Value/100;
        }

        private void ProgressSlider_GotMouseCapture(object sender, MouseEventArgs e)
        {
            mediaPlayer.Pause();
            this.timer1.Stop();
        }

        private void ProgressSlider_LostMouseCapture(object sender, MouseEventArgs e)
        {
            mediaPlayer.Position = TimeSpan.FromMilliseconds(Convert.ToInt32(ProgressSlider.Value));
            mediaPlayer.Play();
            this.timer1.Start();
        }
    }
}
