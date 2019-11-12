using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;



namespace Nasa
{
    class ApplicationServer
    {
        private readonly WebServer webserver = new WebServer();
        private readonly FileServer fileServer = new FileServer();
        private dynamic stuff;
        readonly Window1 SearchResult = new Window1();
        public int itemSize = 300;
        public string SearchDir;
        public async void Search(string SearchTitle, string mediaType,int page)
        {
            //MessageBox.Show(mediaType);
            SearchResult.Title = SearchTitle;
            SearchResult.Topmost = true;
            SearchResult.WindowState = WindowState.Maximized;
            SearchResult.Show();
            SearchResult.Focus();
            SearchResult.Topmost = false;
            SearchDir = SearchTitle;
            fileServer.CreateDir(SearchDir);
            fileServer.CreateAudioDir(SearchDir);
            //stuff = webserver.ConnectToServer(SearchTitle,mediaType,page);
            stuff = await Task.Run(() => webserver.ConnectToServer(SearchTitle, mediaType, page));
            if (mediaType == "&media_type=image")
            {
                SearchResult.SlideShow.Visibility = Visibility.Visible;
            }
            else
            {
                SearchResult.SlideShow.Visibility = Visibility.Hidden;
            }
            if (stuff!=null)
            {
                await Task.Run(() => { InitListTask(); });
            }
            else
            {
                MessageBox.Show("Sorry ,Network Error. Check if your network connection is ok and try again.");
            }
        }

        public async Task InitListTask()
        {
            for (int i = 0; i < (stuff.collection.items.Count); i++)
            {
                ItemData tmpItem = new ItemData();
                Task<ItemData> t0 = Task.Factory.StartNew(() => { return InitListTaskItem(i); });
                tmpItem = t0.Result;
                if (tmpItem.Title != string.Empty)
                {
                    SearchResult.AddToList(await Task.FromResult(tmpItem));
                }
            }
        }
        public ItemData InitListTaskItem(int i)
        {
            if (stuff.collection.items[i].data[0].media_type == "image" || stuff.collection.items[i].data[0].media_type == "video")
            {
                ItemData tmpItem = new ItemData
                {
                    Title = stuff.collection.items[i].data[0].title,
                    href = stuff.collection.items[i].href,
                    center = stuff.collection.items[i].data[0].center,
                    keywords = new List<string> { "Mars" },
                    media_type = stuff.collection.items[i].data[0].media_type,
                    location = stuff.collection.items[i].data[0].location,
                    date_created = stuff.collection.items[i].data[0].date_created,
                    nasa_id = stuff.collection.items[i].data[0].nasa_id,
                    Description = stuff.collection.items[i].data[0].description
                };
                string patch = Convert.ToString(stuff.collection.items[i].links[0].href);
                Task<String> t0 = Task.Factory.StartNew(() => { return fileServer.DownloadImage(patch, SearchDir); });
                tmpItem.ImageDatarget = t0.Result ;
                return (tmpItem);
            }
            else if (stuff.collection.items[i].data[0].media_type == "audio")
            {

                ItemData tmpItem = new ItemData
                {
                    Title = stuff.collection.items[i].data[0].title,
                    href = stuff.collection.items[i].href,
                    center = stuff.collection.items[i].data[0].center,
                    keywords = new List<string> { "Mars" },
                    media_type = stuff.collection.items[i].data[0].media_type,
                    location = stuff.collection.items[i].data[0].location,
                    date_created = stuff.collection.items[i].data[0].date_created,
                    nasa_id = stuff.collection.items[i].data[0].nasa_id,
                    Description = stuff.collection.items[i].data[0].description,
                    ImageDatarget = "pack://application:,,,/images/loudspeaker-155807_1280.png"
                };
                return tmpItem;
            }
            else
            {
                return new ItemData();
            }
        }
    }
}
