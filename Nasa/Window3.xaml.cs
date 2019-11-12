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
using RestSharp;
using Newtonsoft.Json;
using System.Net;

namespace Nasa
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        private readonly ImageSet imageSet = new ImageSet();
        private readonly WebServer webserver = new WebServer();
        public void SetTitle(string Title)
        {
            this.ItemTitle.Text = Title;
        }

        public void SetHref(string href)
        {
            dynamic stuff = webserver.OpenLink(href);
            PoluteResolutionBoxItemSet(stuff);
            ChoseImage();
        }

        private void PoluteResolutionBoxItemSet( dynamic stuff)
        {
            foreach (string item in stuff)
            {
                if (item.Contains("large"))
                {
                    imageSet.large = Convert.ToString(item);
                    ResolutionBox.Items.Add(imageSet.large);
                }
                if (item.Contains("medium"))
                {
                    imageSet.medium = Convert.ToString(item);
                    ResolutionBox.Items.Add(imageSet.medium);
                }
                if (item.Contains("orig"))
                {
                    imageSet.orig = Convert.ToString(item);
                    ResolutionBox.Items.Add(imageSet.orig);
                }
                if (item.Contains("small"))
                {
                    imageSet.small = Convert.ToString(item);
                    ResolutionBox.Items.Add(imageSet.small);
                }
                if (item.Contains("thumb"))
                {
                    imageSet.thumb = Convert.ToString(item);
                    ResolutionBox.Items.Add(imageSet.thumb);
                }
            }
        }

        private void ChoseImage()
        {
            if(imageSet.large != null)
            {
                SelectImage(imageSet.large);
            }
            else if (imageSet.medium != null)
            {
                SelectImage(imageSet.medium);
            }
            else if (imageSet.orig != null)
            {
                SelectImage(imageSet.orig);
            }
            else if (imageSet.small != null)
            {
                SelectImage(imageSet.small);
            }
            else if (imageSet.thumb != null)
            {
                SelectImage(imageSet.thumb);
            }
        }

        private void SelectImage(string img)
        {
            this.SetImage(new BitmapImage(new Uri(img)));
            ResolutionBox.SelectedItem = img;
        }

        public void SetImage(BitmapImage img)
        {
            this.ItemImage.Source = img;
            this.ItemImage.Stretch = Stretch.UniformToFill;
            this.ItemImage.Opacity = 0;
            this.MainDock.Background = new ImageBrush(img);
            
        }
        public void SetDescription(String Description)
        {
            ItemDescription.Text = Description;
        }

        public void SetItemCenter(string itemCenter)
        {
            ItemCenter.Text = itemCenter;
        }

        public void SetItemLocation(string itemLocation)
        {
            ItemLocation.Text = itemLocation;
        }

        public void SetItemKeywords(List<string> itemKeywords)
        {
            ItemKeywords.Text = string.Join(",",itemKeywords) ;
        }
        public void SetItemNasa_id(string itemNasa_id)
        {
            ItemNasa_id.Text = itemNasa_id;
        }

        public void SetItemDate_created(string itemDate_created)
        {
            ItemDate_created.Text = itemDate_created;
        }

        public Window3()
        {
            InitializeComponent();
        }
        private void Full_Screen_Click_1(object sender, RoutedEventArgs e)
        {
            if (this.WindowStyle == WindowStyle.SingleBorderWindow)
            {
                this.WindowStyle = WindowStyle.None;
            }
            else
            {
                this.WindowStyle = WindowStyle.SingleBorderWindow;
            }
            this.WindowState = WindowState.Maximized;
            this.Activate();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }

        private void ResolutionBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ResolutionBox.SelectedItem.ToString() != string.Empty)
            {
                this.SetImage(new BitmapImage(new Uri(this.ResolutionBox.SelectedItem.ToString())));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
