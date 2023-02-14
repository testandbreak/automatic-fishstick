
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SampleTestFramework.Drivers
{
    public record ResourceData : Payload
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id;
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name;
        [JsonProperty("year", NullValueHandling = NullValueHandling.Ignore)]
        public string Year;
        [JsonProperty("colour", NullValueHandling = NullValueHandling.Ignore)]
        public string Colour;
        [JsonProperty("pantone_value", NullValueHandling = NullValueHandling.Ignore)]
        public string PantoneValue;
    }

    public record ResourceSingleData : Payload
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public List<ResourceData> Data;
    }
}