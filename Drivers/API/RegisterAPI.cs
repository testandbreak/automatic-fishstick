using RestSharp;


namespace SampleTestFramework.Drivers
{
    public class RegisterAPI : AuthenticationAPI
    {

        public RegisterAPI()
        {
            resourceName = @"register";
        }

        public (RestResponse response, Payload payload) Register(Credentials credentials)
        {
            return Authenticate(credentials);
        }

    }
}
