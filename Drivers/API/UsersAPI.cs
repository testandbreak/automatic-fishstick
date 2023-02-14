using System;
using System.Net;
using Newtonsoft.Json;
using RestSharp;

namespace SampleTestFramework.Drivers
{
    public class UsersAPI : BaseAPI
    {

        protected string resourceName = @"users";

        public (RestResponse response, Payload payload) List(int? page, int? perPage)
        {
            var request = new RestRequest(resourceName, Method.Get);
            if (page != null)
                request.AddQueryParameter("page", Convert.ToString(page));
            if (perPage != null)
                request.AddQueryParameter("per_page", Convert.ToString(perPage));
            var response = Send(request);

            Payload payload = null;
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                payload = JsonConvert.DeserializeObject<UserPageData>(response.Content);
            }
            else if (Enum.IsDefined(typeof(HttpStatusCode), response.StatusCode))
            {
                payload = JsonConvert.DeserializeObject<ErrorMessage>(response.Content);
            }
            return (response, payload);
        }

        public (RestResponse response, Payload payload) Get(int userId)
        {
            var request = new RestRequest(resourceName+"/{id}", Method.Get).AddUrlSegment("id",userId);
            var response = Send(request);

            Payload payload = null;
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                payload = JsonConvert.DeserializeObject<UserSingleData>(response.Content);
            }
            else
            {
                payload = JsonConvert.DeserializeObject<ErrorMessage>(response.Content);
            }
            return (response, payload);
        }

        public RestResponse Delete(int userId)
        {
            var request = new RestRequest(resourceName+"/{id}", Method.Delete).AddUrlSegment("id",userId);
            var response = Send(request);

            return response;
        }

        
        public (RestResponse response, Payload payload) Create(UserData userData)
        {
            var request = new RestRequest(resourceName, Method.Post).AddJsonBody<UserData>(userData);
            var response = Send(request);

            Payload payload = null;
            if (response.StatusCode.Equals(HttpStatusCode.Created))
            {
                payload = JsonConvert.DeserializeObject<UserData>(response.Content);
            }
            else if (Enum.IsDefined(typeof(HttpStatusCode), response.StatusCode))
            {
                payload = JsonConvert.DeserializeObject<ErrorMessage>(response.Content);
            }

            return (response, payload);
        } 
    }
}
