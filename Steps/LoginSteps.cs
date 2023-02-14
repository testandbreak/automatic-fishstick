using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;
using FluentAssertions;

using SampleTestFramework.Drivers;

namespace SampleTestFramework.Steps
{
    [Binding]
    public sealed class LoginSteps : BaseSteps
    {
        private LoginAPI api;

        public LoginSteps(ScenarioContext scenarioContext, ISpecFlowOutputHelper outputHelper) : base(scenarioContext,outputHelper)
        {
            api = new LoginAPI();
        }

        [When(@"requesting to login with my credentials")]
        public void RequestingToLoginWithMyCredentials()
        {
            Credentials credentials = GetScenarioData<Credentials>("credentials");
            (var response, var payload) = api.Login(credentials);

            Log(api.AsString(response.Request));
            Log(api.AsString(credentials));
            Log(api.AsString(response));

            AddScenarioData("response", response);
            AddScenarioData("payload", payload);
        }
    }
}