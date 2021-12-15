using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace CatProvider
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient HttpClient;
        public HttpClientWrapper()
        {
            HttpClient = new HttpClient();
            HttpClient.DefaultRequestHeaders.Add("x-api-key", "f843d100-fdbd-4df1-b181-aef713912f0a");
        }
        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await HttpClient.GetAsync(url);
        }
        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            return await HttpClient.PostAsync(url, content);
        }
        public void Dispose()
        {
            HttpClient.Dispose();
        }
    }
}
