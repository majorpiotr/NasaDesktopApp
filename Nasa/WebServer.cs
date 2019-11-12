using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using System.Net;


namespace Nasa
{
    class WebServer
    {
        public async Task<dynamic> ConnectToServer(string SearchTitle, string mediaType,int page)
        {
            try
            {

                string query = "";
                query   +=  "/search?keywords={" + SearchTitle.ToString() + "}";
                query   +=  mediaType;
                query   += "&page="+page.ToString();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var client = new RestClient("https://images-api.nasa.gov");
                var request = new RestRequest(query, RestSharp.DataFormat.Json);
                var response = client.Get(request);
                return await Task.FromResult(JsonConvert.DeserializeObject(response.Content));
            }
            catch
            {
                return null;
            }
        }

        public dynamic OpenLink( string href)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = new RestClient(href);
            var request = new RestRequest("", RestSharp.DataFormat.Json);
            var response = client.Get(request);
            return JsonConvert.DeserializeObject(response.Content);
        }
    }
}
