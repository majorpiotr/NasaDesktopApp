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

namespace Nasa
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            Properties.Settings settings = Properties.Settings.Default;
            SearchText.Text = settings.LastQuery;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Properties.Settings settings = Properties.Settings.Default;
            settings.LastQuery = SearchText.Text;
            settings.Save();

            if (this.CheckBoxAudios.IsChecked.Value ==this.CheckBoxPhotos.IsChecked.Value && this.CheckBoxPhotos.IsChecked.Value == this.CheckBoxVideos.IsChecked.Value )
            {
                ApplicationServer applicationServer = new ApplicationServer();
                applicationServer.Search(this.SearchText.Text, "",1);
            }
            else 
            {
                    List<string> query = new List<string>();
                    if(this.CheckBoxAudios.IsChecked.Value == true)
                    { query.Add("media_type=audio"); }
                    if(this.CheckBoxPhotos.IsChecked.Value == true)
                    { query.Add("media_type=image"); }
                    if(this.CheckBoxVideos.IsChecked.Value == true)
                    { query.Add("media_type=video"); }
                    ApplicationServer applicationServer = new ApplicationServer();
                    applicationServer.Search(this.SearchText.Text, "&"+string.Join("&", query),1);
            }
        }

        private void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(this.SearchText.Text.Length==0 )
            {
                if (SearchBtn.IsEnabled == true)
                {
                    SearchBtn.IsEnabled = false;
                    SearchBtn.Opacity = 0;
                }
            }
            else
            {
                if(SearchBtn.IsEnabled == false)
                {
                    SearchBtn.IsEnabled = true;
                    SearchBtn.Opacity = 1;
                }
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void CheckBoxPhotos_Checked(object sender, RoutedEventArgs e)
        {
            CheckBoxSpaceOnly.Visibility =Visibility.Visible;
        }

        private void CheckBoxPhotos_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBoxSpaceOnly.Visibility = Visibility.Hidden;
            CheckBoxSpaceOnly.IsChecked = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
