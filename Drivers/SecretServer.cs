namespace SampleTestFramework.Drivers {

    public class SecretServerClient
    {
        private static readonly string SecretServerAPIURL;
        private static readonly string SecretServerKey;

        static SecretServerClient()
        {
            SecretServerKey = EnvironmentConfig.EnvVar("QA_SECRETSERVER_APIKEY");
            SecretServerAPIURL = EnvironmentConfig.Item("SecretServerAPIURL");
        }

        public static string GetSecret(string scope, string domain, string secretName)
        {
            // Here is where you could put code to retrieve the auditable access to secrets
            return "aSecretString";
        }
    }
}