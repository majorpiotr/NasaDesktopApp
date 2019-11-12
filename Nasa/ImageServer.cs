using RestSharp;
using Newtonsoft.Json;
using System.Net;

namespace Nasa
{
    class ImageServer
    {
        public string getImages(string href)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = new RestClient(href);
            var request = new RestRequest("", RestSharp.DataFormat.Json);
            var response = client.Get(request);
            dynamic stuff = JsonConvert.DeserializeObject(response.Content);
            return stuff[0];
        }
    }
}
