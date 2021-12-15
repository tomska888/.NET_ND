using Newtonsoft.Json;

namespace CatProvider.Models
{
    public class Favourite
    {
        [JsonProperty("image_id")]
        public string ImageId { get; set; }   
        [JsonProperty("sub_id")]
        public string SubId { get; set; }
    }
}
