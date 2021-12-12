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
            var data = await catService.GetBreed("A");
            var data2 = await catService.GetImage("thumb");
        }
    }
}
