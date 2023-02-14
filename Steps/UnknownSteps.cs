using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

using SampleTestFramework.Drivers;

namespace SampleTestFramework.Steps
{
    [Binding]
    public sealed class UnknownSteps : BaseSteps
    {
        private UnknownAPI api;

        public UnknownSteps(ScenarioContext scenarioContext, ISpecFlowOutputHelper outputHelper) : base(scenarioContext,outputHelper)
        {
            api = new UnknownAPI();
        }

        [Given(@"the resource id (\d+)")]
        public void TheResourceId(int resourceId)
        {
            AddScenarioData("resourceId", resourceId);
        }

        [When(@"requesting to get the data for the specific unknown resource")]
        public void RequestingToGetTheDataForTheSpecificUnknownResource()
        {
            (var response, var payload) = api.Get(GetScenarioData<int>("resourceId"));

            Log(api.AsString(response.Request));
            Log(api.AsString(response));

            SetScenarioData("response", response);
            if (payload is UserSingleData)
                AddScenarioData("resourceData", ((ResourceSingleData)payload).Data);
            else
                AddScenarioData("payload", payload);
        }
    }
}