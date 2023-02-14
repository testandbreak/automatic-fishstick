using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;
using FluentAssertions;

using SampleTestFramework.Drivers;

namespace SampleTestFramework.Steps
{
    [Binding]
    public sealed class AuthenticationSteps : BaseSteps
    {
        public AuthenticationSteps(ScenarioContext scenarioContext, ISpecFlowOutputHelper outputHelper) : base(scenarioContext,outputHelper)
        {
        }

        // This step would be used for non-secure platforms for max flexibiliy/agility
        [Given(@"the credentials \((.*),(.*)\)")]
        public void TheCredentials(string email, string password)
        {
            AddScenarioData("credentials", new Credentials(email, password));
        }

        // This step would be used for secure platforms where there is a requirement to
        // keep passwords (or other details hidden)
        [Given(@"the secret credentials for (.*)")]
        public void TheSecretCredentialsFor(string email)
        {
            AddScenarioData("credentials", new Credentials(email, SecretServerClient.GetSecret("QA", "UserAccount.Password", email)));
        }

        [Then(@"the error message is '(.*)'")]
        public void TheErrorMessageIs(string expectedErrorMessage)
        {
            var errorMessage = GetScenarioData<ErrorMessage>("payload");
            errorMessage.Error.Should().Be(expectedErrorMessage);
        }

        [Then(@"the authentication response contains a token")]
        public void TheResponseContainsAnAuthenticatedToken()
        {
            var authToken = GetScenarioData<AuthToken>("payload");
            authToken.Should().NotBeNull();
            authToken.Token.Should().NotBeNullOrEmpty();
        }

        [Then(@"the authentication response contains the id (\d+)")]
        public void TheResponseContainsAnAuthenticatedId(int id)
        {
            var authToken = GetScenarioData<AuthToken>("payload");
            authToken.Should().NotBeNull();
            authToken.Id.Should().Be(id);
        }

    }
}