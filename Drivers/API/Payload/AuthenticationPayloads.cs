
using Newtonsoft.Json;

namespace SampleTestFramework.Drivers
{
    public record Credentials : Payload
    {
        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email;

        [JsonProperty("password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password;

        [JsonProperty("username", NullValueHandling = NullValueHandling.Ignore)]
        public string Username;

        public Credentials(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }
    }

    public record AuthToken : Payload
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int Id;

        [JsonProperty("token")]
        public string Token;
    }
}