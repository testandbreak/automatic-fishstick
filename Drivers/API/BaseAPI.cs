using FluentAssertions;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace SampleTestFramework.Drivers
{
    public class BaseAPI
    {
        RestClient restClient;


        public BaseAPI()
        {  
            SetUpRestClient(EnvironmentConfig.Item("BaseAPIURL"));
        }

        public BaseAPI(string baseUrl) {
            SetUpRestClient(baseUrl);
        }

        protected void SetUpRestClient(string baseUrl)
        {
            restClient = new RestSharp.RestClient(baseUrl);
            restClient.UseNewtonsoftJson();
        }

        public RestResponse Send(RestRequest request) {
            var response = restClient.Execute(request);
            if (response.ResponseStatus != ResponseStatus.Completed 
                && response.ResponseStatus != ResponseStatus.Error 
                && response.StatusCode == 0)
            {
                System.Console.WriteLine("HTTP Transport Error: ResponseStatus("+response.ResponseStatus+") : ErrorMessage("+response.ErrorMessage+")");
                System.Console.WriteLine("HTTP Transport Exception: "+response.ErrorException);
                System.Console.WriteLine("HTTP Resposne: StatusCode("+response.StatusCode+") : StatusDescription("+response.StatusDescription+")");
            }
            response.ResponseStatus.Should().BeOneOf(ResponseStatus.Completed, ResponseStatus.Error);
            response.StatusCode.Should().NotBe(0, "because that means the API didn't respond");
            return response;
        }

        public string AsString(RestRequest request)
        {
            string text = "Request.Url: "+restClient.BuildUri(request)+"\n";
            //foreach (var param in request.Parameters.GetParameters(ParameterType.RequestBody))
            //{
            //    text+= "Request.Body: \n"+JsonConvert.SerializeObject(param.Value, Formatting.Indented)+"\n\n";
            //}
            return text;
        }

        public string AsString(RestResponse response)
        {
            string text = "Response.StatusCode: "+response.StatusCode+"\n";
            text +="Response.Body: \n"+response.Content+"\n\n";
            return text;
        }

        public string AsString(Payload payload)
        {
            string text = "Payload.Type: "+payload.GetType().FullName+"\n";
            text +="Payload.AsJson: \n"+JsonConvert.SerializeObject(payload, Formatting.Indented)+"\n\n";
            return text;
        }

        public string AsString(Credentials payload)
        {
            payload.Password = "********";
            string text = "Payload.Type: "+payload.GetType().FullName+"\n";
            text +="Payload.AsJson: \n"+JsonConvert.SerializeObject(payload, Formatting.Indented)+"\n\n";
            return text;
        }
    }
}
