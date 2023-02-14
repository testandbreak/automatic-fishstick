using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;
using FluentAssertions;

using SampleTestFramework.Drivers;

namespace SampleTestFramework.Steps
{
    [Binding]
    public sealed class RegisterSteps : BaseSteps
    {
        private RegisterAPI api;

        public RegisterSteps(ScenarioContext scenarioContext, ISpecFlowOutputHelper outputHelper) : base(scenarioContext,outputHelper)
        {
            api = new RegisterAPI();
        }

        [When(@"requesting to register with my credentials")]
        public void RequestingToRegisterWithMyCredentials()
        {
            Credentials credentials = GetScenarioData<Credentials>("credentials");
            (var response, var payload) = api.Register(credentials);

            Log(api.AsString(response.Request));
            Log(api.AsString(credentials));
            Log(api.AsString(response));

            AddScenarioData("response", response);
            AddScenarioData("payload", payload);
        }

    }
}