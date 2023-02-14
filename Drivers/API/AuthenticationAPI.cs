using System;
using System.Net;
using Newtonsoft.Json;
using RestSharp;


namespace SampleTestFramework.Drivers
{
    public class AuthenticationAPI : BaseAPI
    {
        protected string resourceName;

        public (RestResponse response, Payload payload) Authenticate(Credentials credentials)
        {
            var response =  Send(new RestRequest(resourceName, Method.Post).AddJsonBody<Credentials>(credentials));

            Payload payload = null;
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                payload = JsonConvert.DeserializeObject<AuthToken>(response.Content);
            }
            else if (Enum.IsDefined(typeof(HttpStatusCode), response.StatusCode))
            {
                payload = JsonConvert.DeserializeObject<ErrorMessage>(response.Content);
            }
            return (response, payload);
        }

    }
}
