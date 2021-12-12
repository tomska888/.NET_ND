using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CatProvider
{
    public interface IHttpClientWrapper : IDisposable
    {
        Task<HttpResponseMessage> GetAsync(string url);
        //Task<HttpResponseMessage> PostAsync(string url);
    }
}