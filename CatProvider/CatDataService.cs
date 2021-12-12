﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CatProvider.Models;

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
        public async Task<List<Breed>> GetBreed(string q)
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
    }
}