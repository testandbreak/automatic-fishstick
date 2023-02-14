using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

namespace SampleTestFramework.Steps
{
    public class BaseSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly ISpecFlowOutputHelper _outputHelper;

        public BaseSteps(ScenarioContext scenarioContext, ISpecFlowOutputHelper outputHelper)
        {
            _scenarioContext = scenarioContext;
            _outputHelper = outputHelper;
        }
        
        public void AddScenarioData(string key, object obj)
        {
            _scenarioContext.Add(key, obj);
        }

        public void SetScenarioData(string key, object obj)
        {
            _scenarioContext.Set(obj, key);
        }

        public void SetScenarioData<T>(string key, T obj)
        {
            _scenarioContext.Set<T>(obj, key);
        }

        public T GetScenarioData<T>(string key)
        {
            return _scenarioContext.Get<T>(key);
        }

        /// <summary>
        /// This method is a simplistic way to absract logging, which
        /// in this case is nothing more than the livingdoc logging
        /// </summary>
        public void Log(string entry)
        {
            _outputHelper.WriteLine(entry);
        }

    }
}