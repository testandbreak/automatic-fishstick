
using Newtonsoft.Json;

namespace SampleTestFramework.Drivers
{

    public record Payload;

    public record ErrorMessage : Payload
    {
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public string Error;
    };
}