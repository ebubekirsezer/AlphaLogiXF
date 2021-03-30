using System;
using Newtonsoft.Json;

namespace AlphaLogi.Models
{
    public class BoundingBox
    {
        [JsonProperty("left")]
        public double Left { get; set; }

        [JsonProperty("top")]
        public double Top { get; set; }

        [JsonProperty("width")]
        public double Width { get; set; }

        [JsonProperty("height")]
        public double Height { get; set; }
    }
}
