using System.Collections.Generic;
using Newtonsoft.Json;

namespace SampleTestFramework.Drivers
{
    public record UserData : Payload
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int? Id;

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name;

        [JsonProperty("job", NullValueHandling = NullValueHandling.Ignore)]
        public string Job;

        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty("first_name", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }

        [JsonProperty("last_name", NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }

        [JsonProperty("avatar", NullValueHandling = NullValueHandling.Ignore)]
        public string Avatar { get; set; }

        [JsonProperty("createdAt", NullValueHandling = NullValueHandling.Ignore)]
        public string CreatedAt;

        [JsonProperty("updatedAt", NullValueHandling = NullValueHandling.Ignore)]
        public string UpdatedAt;
    }

    public record UserSingleData : Payload
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public UserData Data;
    }

    public record UserPageData : Payload
    {
        [JsonProperty("page", NullValueHandling = NullValueHandling.Ignore)]
        public int Page;

        [JsonProperty("per_page", NullValueHandling = NullValueHandling.Ignore)]
        public int PerPage;

        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public int Total;

        [JsonProperty("total_pages", NullValueHandling = NullValueHandling.Ignore)]
        public int TotalPages;

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public List<UserData> Data;
    }
}