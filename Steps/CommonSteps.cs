using System.Net;
using TechTalk.SpecFlow;
using FluentAssertions;
using RestSharp;
using System;
using TechTalk.SpecFlow.Infrastructure;

namespace SampleTestFramework.Steps 
{
    [Binding]
    public class CommonSteps : BaseSteps
    {

        public CommonSteps(ScenarioContext scenarioContext, ISpecFlowOutputHelper outputHelper) : base(scenarioContext,outputHelper)
        {
        }


        [Then(@"the returned HTTP response code is (\d+)")]
        public void TheReturnedHTTPStatusCodeShouldBe(HttpStatusCode httpStatusCode)
        {
            var returnedStatusCode = GetScenarioData<RestResponse>("response").StatusCode;
            returnedStatusCode.Should().Be(httpStatusCode);
        }
    }
}