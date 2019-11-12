using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Nasa
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Boolean done = false;
        public int directory = 1;
        public List<string> ImageList = new List<string>();
        public int currentPhoto = 0;
        public int currentPhoto2 = 1;
        private DispatcherTimer timer1;
        private readonly ImageServer webServer = new ImageServer();
        public void append(string toAppend)
        {
            this.ImageList.Add(toAppend);
        }
        private BitmapImage LoadImage(string filename)
        {
            BitmapImage ToReturn = new BitmapImage(new Uri(filename));
            //ToReturn.Freeze();
            return ToReturn;
        }

        public Window2()
        {
            InitializeComponent();
        }
        public void Start()
        {
            this.SlideShow.Source = LoadImage(webServer.getImages(ImageList[currentPhoto]));
            this.SlideShow2.Source = LoadImage(webServer.getImages(ImageList[currentPhoto2]));
        }

        public void FullScreen()
        {
            this.SlideShow.Width = this.ActualWidth;
            this.SlideShow.Height = this.ActualHeight;
            this.SlideShow.Stretch = Stretch.UniformToFill ;
            this.SlideShow2.Width = this.ActualWidth;
            this.SlideShow2.Height = this.ActualHeight;
            this.SlideShow2.Stretch = Stretch.UniformToFill;
            InitTimer();
        }

        
        public void InitTimer()
        {
            timer1 = new DispatcherTimer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = new TimeSpan(0, 0, 0, 0,2); 
            timer1.Start();
        }
        public void SlideShowChange()
        {
            this.SlideShow.Source = LoadImage(webServer.getImages(ImageList[currentPhoto]));
        }

        public void SlideShow2Change()
        {
            this.SlideShow2.Source = LoadImage(webServer.getImages(ImageList[currentPhoto2]));
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            if (directory == 1)
            {
                this.SlideShow.Opacity -= 0.001;
                if (this.SlideShow.Opacity < -0.25)
                {
                    directory = 0;
                    if (currentPhoto< ImageList.Count-2)
                    {
                        currentPhoto+=2;
                    }
                    else
                    {
                        currentPhoto = 0;
                    }
                    SlideShowChange();
                }
            }
            else
            {
                this.SlideShow.Opacity += 0.001;
                if (this.SlideShow.Opacity > 1.25)
                {
                    directory = 1;
                    if (currentPhoto < ImageList.Count-2)
                    {
                        currentPhoto2+=2;
                    }
                    else
                    {
                        currentPhoto2 = 0;
                    }
                    SlideShow2Change();
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                Close();
            }
        }
    }
}
