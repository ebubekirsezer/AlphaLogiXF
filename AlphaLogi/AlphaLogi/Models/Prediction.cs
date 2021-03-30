using System;
using Newtonsoft.Json;

namespace AlphaLogi.Models
{
    public class Prediction
    {
        [JsonProperty("probability")]
        public double Probability { get; set; }

        [JsonProperty("tagId")]
        public Guid TagId { get; set; }

        [JsonProperty("tagName")]
        public string TagName { get; set; }

        [JsonProperty("boundingBox")]
        public BoundingBox BoundingBoxBoundingBox { get; set; }
    }
}
