using RestSharp;


namespace SampleTestFramework.Drivers
{
    public class LoginAPI : AuthenticationAPI
    {

        public LoginAPI()
        {
            resourceName = @"login";
        }


        public (RestResponse response, Payload payload) Login(Credentials credentials)
        {
            return Authenticate(credentials);
        }

    }
}
