using System.Net.Http;
using System.Threading.Tasks;
using finisher_service.api;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using TechTalk.SpecFlow;

namespace aidantwomey.src.dotnetcore.handicap_timer.finisher_service.tests.finisher_service.tests.Steps
{
    [Binding]
    public sealed class CalculatorStepDefinitions
    {
        private readonly HttpClient client;
        private HttpResponseMessage response;

        public CalculatorStepDefinitions(WebApplicationFactory<Startup> factory)
        {
            client = factory.CreateClient();
        }

        [Given("the service is initialised")]
        public void GivenTheServiceIsInitialised()
        {
        }

        [When("the health endpoint is called")]
        public async Task WhenTheHealthEndpointIsCalled()
        {
            response = await client.GetAsync("/healthz");
        }

        [Then("the response should be (.*)")]
        public void ThenTheResultShouldBe(int result)
        {
            response.EnsureSuccessStatusCode();
        }
    }
}