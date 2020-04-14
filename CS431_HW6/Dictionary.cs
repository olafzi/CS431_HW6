using Newtonsoft.Json;

namespace CS431_HW6
{
    public partial class Dictionary
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("definition")]
        public string Definition { get; set; }

        [JsonProperty("example")]
        public string Example { get; set; }
    }
}
