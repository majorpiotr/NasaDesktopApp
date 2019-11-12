using System;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using System.IO;

namespace Nasa
{
    class FileServer
    {
        public string DownloadImage( string item, string directory )
        {
            if (!File.Exists(@"C:\nasa\images\" + directory + @"\" + item.Split( Convert.ToChar("/") ).Last()))
            {
                var client = new RestClient(item);
                var request = new RestRequest("", RestSharp.DataFormat.None);
                byte[] response = client.DownloadData(request);
                File.WriteAllBytes(@"C:\nasa\images\" + directory + @"\"+ item.Split(Convert.ToChar("/")).Last(), response);
            }
            return @"C:\nasa\images\" + directory + @"\" + item.Split(Convert.ToChar("/")).Last();
        }

        public async Task<string> DownloadAudio(string item )
        {
            if (!File.Exists(@"C:\nasa\audio\"  + item.Split(Convert.ToChar("/")).Last()))
            {
                var client = new RestClient(item);
                var request = new RestRequest("", RestSharp.DataFormat.None);
                byte[] response = client.DownloadData(request);
                File.WriteAllBytes(@"C:\nasa\audio\"+ item.Split(Convert.ToChar("/")).Last() , response);
            }
            return await Task.FromResult(@"C:\nasa\audio\" + item.Split(Convert.ToChar("/")).Last());
        }

        public void CreateDir(string dirName)
        {
            string path = @"C:\nasa\images\"+ dirName;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        public void CreateAudioDir(string dirName)
        {
            string path = @"C:\nasa\audio\" + dirName;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
