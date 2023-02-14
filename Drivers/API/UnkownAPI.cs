using System;
using System.Net;
using Newtonsoft.Json;
using RestSharp;

namespace SampleTestFramework.Drivers
{
    public class UnknownAPI : BaseAPI
    {
        protected string resourceName = @"unknown";

        public (RestResponse response, Payload payload) Get(int resourceId)
        {
            var request = new RestRequest(resourceName+"/{id}", Method.Get).AddUrlSegment("id",resourceId);
            var response = Send(request);

            Payload payload = null;
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                payload = JsonConvert.DeserializeObject<ResourceData>(response.Content);
            }
            return (response, payload);
        }

    }
}
