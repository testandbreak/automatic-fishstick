using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

namespace SampleTestFramework.Hooks
{
    [Binding]
    public class Hooks
    {

        [BeforeTestRun]
        public static void SetupBaseSystemData(ISpecFlowOutputHelper outputHelper)
        {
            // Could use this to start up a local version of the system if using a specifc env
            //   and/or
            // Use alternate route to prime system databases etc with base set of data
            // Data could be sourced from automation e.g. .\Drivers\Data\*.csv and then it
            // could be loaded by tests to use as oracle/pool of known data.
        }

        [AfterTestRun]
        public static void CleanOutBaseSystemData()
        {
            // Clean data from system to return to initial state
            //   and/or
            // Could also use to shutdown the services
        }

    }
}
