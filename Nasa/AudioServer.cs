using System;
using System.Threading.Tasks;


namespace Nasa
{
    class AudioServer
    {
        public string curentFile;
        private readonly FileServer fileServer = new FileServer();
        private readonly WebServer webserver = new WebServer();
        private readonly AudioFileList audioFileList = new AudioFileList();

        public async Task SetHref(string href)
        {
            dynamic stuff = webserver.OpenLink(href);
            foreach (string item in stuff)
            {
                if (item.Contains("orig.mp3"))
                {
                    audioFileList.orig = await Task.FromResult(item);
                }
                else if (item.Contains("128k.mp3"))
                {
                    audioFileList.mp3 = await Task.FromResult(item);
                }
                else if (item.Contains("metadata.json"))
                {
                    audioFileList.metadata = await Task.FromResult(item);
                }
            }
        }
        public async Task<string> DownloadFile()
        {
            this.curentFile = await Task.Run(() => fileServer.DownloadAudio(audioFileList.mp3));
            //fileServer.DownloadAudio(audioFileList.mp3);
            return await Task.FromResult(this.curentFile);
        }

        public async Task<double> SetDuration()
        {
            var tfile = TagLib.File.Create(curentFile);
            TimeSpan duration = tfile.Properties.Duration;
            tfile.Dispose();
            return await Task.FromResult(duration.TotalMilliseconds);
        }


    }
}
