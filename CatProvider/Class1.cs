using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CatProvider
{
    public interface IHttpClientWrapper : IDisposable
    {
        Task<HttpResponseMessage> GetAsync(string url);
    }

    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient HttpClient;
        public HttpClientWrapper()
        {
            HttpClient = new HttpClient();
        }
        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await HttpClient.GetAsync(url);
        }
        public void Dispose()
        {
            HttpClient.Dispose();
        }
    }


    public class CatDataService
    {
        private readonly IHttpClientWrapper HttpClient;
        private readonly UriBuilder BaseUrl;

        public CatDataService(IHttpClientWrapper httpClient, string url)
        {
            HttpClient = httpClient;
            BaseUrl = new UriBuilder($"{url}/v1/breeds/search");
        }
        public async Task<List<Breed>> GetBreedsName(string q)
        {
            BaseUrl.Query = $"q={q}";

            var response = await HttpClient.GetAsync(BaseUrl.ToString());
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Breed>>(json);
        }
    }

    public class CatDataService2
    {
        private readonly IHttpClientWrapper HttpClient;
        private readonly UriBuilder BaseUrl;

        public CatDataService2(IHttpClientWrapper httpClient, string url)
        {
            HttpClient = httpClient;
            BaseUrl = new UriBuilder($"{url}/v1/categories");
        }
        public async Task<List<Category>> GetBreedsName(string q)
        {
            var response = await HttpClient.GetAsync(BaseUrl.ToString());
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Category>>(json);
        }
    }
}
