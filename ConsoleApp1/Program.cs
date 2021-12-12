using CatProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
            Console.Read();
        }

        static async Task MainAsync(string[] args)
        {
            var httpClient = new HttpClientWrapper();
            var catService = new CatDataService(httpClient);
            var BreedsList = await catService.GetBreed("A");
            var Image = await catService.GetImage("thumb");
            var Category = await catService.GetCategory(1, 2);
        }
    }
}
