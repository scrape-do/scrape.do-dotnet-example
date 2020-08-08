using System;
using System.Net;
using System.Net.Http;

namespace scrape.do_dotnet_example
{
    class Program
    {
        private readonly static string _apiToken = "";
        private readonly static string _geoCode = "";
        private readonly static string _session = "";
        private readonly static bool _jsRender = false;
        private readonly static bool _superProxy = false;

        /// <summary>
        /// Please browse the documents(https://docs.scrape.do/)
        /// </summary>
        /// <returns></returns>
        private static string CreateRequestUrl(string targetUrl)
        {
            if (string.IsNullOrEmpty(_apiToken))
                throw new Exception("API_TOKEN cannot be empty!");

            string url = $"http://api.scrape.do?token={_apiToken}";

            if (_jsRender)
                url += "&render=true";
            if (_superProxy)
                url += "&super=true";
            if (!string.IsNullOrEmpty(_geoCode))
                url += $"&geoCode={_geoCode}";
            if (!string.IsNullOrEmpty(_session))
                url += $"&session={_session}";

            return $"{url}&url={targetUrl}";

        }
        static void Main(string[] args)
        {

            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            var client = new HttpClient(handler);

            string requestUrl = CreateRequestUrl("https://example.com");
            var response = client.GetAsync(requestUrl).Result;

            Console.WriteLine("Status Code : {0} ", response.StatusCode);
            Console.WriteLine("HTML : \n{0}", response.Content.ReadAsStringAsync().Result);

            Console.ReadLine();
        }
    }
}
