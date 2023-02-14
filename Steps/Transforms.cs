using System;
using System.Net;
using TechTalk.SpecFlow;


namespace SampleTestFramework.Steps
{

    // Used to transform between HTTP Response Codes to object instance
    [Binding]
    public class Transforms
    {
        [StepArgumentTransformation(@" HTTP response code is (\d+)")]
        public HttpStatusCode HttpResponseCodeTransform(int code)
        {
            return (HttpStatusCode)Enum.ToObject(typeof(HttpStatusCode), code);
        }
    }
}