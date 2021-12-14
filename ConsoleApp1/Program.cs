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
            var BreedsList = await catService.GetBreedList(1, 60);
            var SreachBreedsList = await catService.GetBreedSearch("a");
            var Image = await catService.GetImage("0j7EpepFB");
            var Category = await catService.GetCategory(1, 2);
        }
    }
}
