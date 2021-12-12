using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatProvider.Models
{
    public class Breed
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Origin { get; set; }
        public string Description { get; set; }
        [JsonProperty("wikipedia_url")]
        public string WikipediaUrl { get; set; }
    }
}
