using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CatProvider.Models;
using System.Linq;

namespace CatProvider
{
    public class CatDataService
    {
        private readonly IHttpClientWrapper _httpClient;
        private UriBuilder Uri;

        public CatDataService(IHttpClientWrapper httpClient)
        {
            _httpClient = httpClient;
            Uri = new UriBuilder("https://api.thecatapi.com");
        }

        public async Task<List<Breed>> GetBreedSearch(string q)
        {
            Uri.Path = "v1/breeds/search";
            Uri.Query = $"q={q}";

            var response = await _httpClient.GetAsync(Uri.ToString());
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Breed>>(json);
        }

        public async Task<List<Image>> GetImage(string size)
        {
            Uri.Path = "v1/images/search";
            Uri.Query = $"size={size}";

            var response = await _httpClient.GetAsync(Uri.ToString());
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Image>>(json);
        }

        public async Task SaveFavorites(string imageId)
        {
            Uri.Path = "v1/favourites";
            var model = new Favourite();
            model.ImageId = imageId;

            var serializedModel = JsonConvert.SerializeObject(model);

            var httpContent = new StringContent(serializedModel, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(Uri.ToString(), httpContent);
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<Favourite>> GetFavourites()
        {
            Uri.Path = "v1/favourites";
            Uri.Query = $"";

            var response = await _httpClient.GetAsync(Uri.ToString());
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Favourite>>(json);
        }

        public async Task<List<Breed>> GetBreedList()
        {
            Uri.Path = "v1/breeds";
            Uri.Query = $"";

            var response = await _httpClient.GetAsync(Uri.ToString());
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Breed>>(json);
        }

        public async Task<List<Category>> GetCategory(int limit, int page)
        {
            Uri.Path = "v1/categories";
            Uri.Query = $"limit={limit}&page={page}";

            var response = await _httpClient.GetAsync(Uri.ToString());
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Category>>(json);
        }
    }
}
