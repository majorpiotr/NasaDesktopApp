using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using RestSharp;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Drawing;
using System.Threading;

using System.Threading.Tasks;
using System.ComponentModel;

namespace Nasa
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        //public string title { get; set; }
        public ObservableCollection<ItemData> SearchedItems = new ObservableCollection<ItemData>();
        public Boolean IsFullyLoaded = false;
        public dynamic stuff;

        public BitmapImage tmpImageIco = new BitmapImage(new Uri("pack://application:,,,/images/iconfinder_icon-camera_211634tmp.png"));
        public List<int> ImageList = new List<int>();
        public int itemSize;

        public Window1()
        {
            InitializeComponent();
            itemSize = Convert.ToInt32(this.ActualWidth) / 121 * 30;//Convert.ToInt32(SizeSlider.Value);
            this.SearchList.ItemsSource = SearchedItems;
        }
        public void InitComboBox()
        {
            PageList.ItemsSource = new List<int> { 1, 2, 3, 4, 5 };
        }

        public void AddToList(ItemData tmpItem)
        {
            Action<ItemData> update = AddToListThread;
            Application.Current.Dispatcher.BeginInvoke(update, tmpItem);
        }
        public void AddToListThread(ItemData tmpItem)
        {
            tmpItem.ImageData = LoadImage(tmpItem.ImageDatarget );
            tmpItem.Size = Convert.ToInt32(this.ActualWidth) / 121 * Convert.ToInt32(SizeSlider.Value);
            SearchedItems.Add(tmpItem);
        }

        private BitmapImage LoadImage(string filename)
        {
            BitmapImage ToReturn = new BitmapImage(new Uri(filename));
            ToReturn.Freeze();
            return ToReturn;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            for(int i =0; i< SearchedItems.Count ; i++)
            {
                SearchedItems[i].Size = Convert.ToInt32(this.ActualWidth) / 121 * Convert.ToInt32(SizeSlider.Value);
            }
            this.SearchList.Items.Refresh();
        }


        private void SearchResultsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            InitComboBox();
            this.CategoryTitle.Text = Title;
            IsFullyLoaded = true;
            //InitList(Title);
        }

        private void SearchResultsWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            
            if (IsFullyLoaded)
            {
                //MessageBox.Show(Convert.ToString(this.ActualWidth));
                for (int i = 0; i < SearchedItems.Count; i++)
                {
                    SearchedItems[i].Size = Convert.ToInt32(this.ActualWidth) / 121 * Convert.ToInt32(SizeSlider.Value);
                }
                this.SearchList.Items.Refresh();
            }
        }


        private void SlideShow_Click(object sender, RoutedEventArgs e)
        {
            Window2 slideShowWindow = new Window2();
            foreach (ItemData item in SearchedItems)
            {
                if(item.media_type=="image")
                {
                    slideShowWindow.append(item.href);
                }
            }

            slideShowWindow.Show();
            slideShowWindow.Start();
            slideShowWindow.WindowState = WindowState.Maximized;
            slideShowWindow.FullScreen();
        }

        private async void SearchList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ItemData Selected = (ItemData)SearchList.SelectedItem;
            if ( Selected.media_type=="image")
            {
                Window3 itemWindow = new Window3();
                //Set Values
                itemWindow.SetTitle(Selected.Title);

                itemWindow.SetHref(Selected.href);

                itemWindow.SetDescription(Selected.Description);
                itemWindow.SetItemCenter(Selected.center);
                //new

                itemWindow.SetItemDate_created(Selected.date_created);
                itemWindow.SetItemNasa_id(Selected.nasa_id);
                itemWindow.SetItemKeywords(Selected.keywords);
                itemWindow.SetItemLocation(Selected.location);

                //Make fullscreen, on front,focused
                itemWindow.Show();
                itemWindow.Activate();
                itemWindow.Topmost = true;
                itemWindow.WindowState = WindowState.Maximized;
                itemWindow.Focus();
            }
            else if(Selected.media_type=="audio")
            {
                
                AudioPlayer audioPlayer = new AudioPlayer();
                audioPlayer.Show();
                
                AudioServer audioServer = new AudioServer();

                await Task.Run(() => audioServer.SetHref(Selected.href));
                string FileUrl  = await Task.Run(() => audioServer.DownloadFile());
                double duration = await Task.Run(() => audioServer.SetDuration());

                audioPlayer.OpenMediafile(FileUrl);
                audioPlayer.SetDuration( duration);
                audioPlayer.SetTimer();
                audioPlayer.SetTitle(Selected.Title);
            }
            else if (Selected.media_type == "video")
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
