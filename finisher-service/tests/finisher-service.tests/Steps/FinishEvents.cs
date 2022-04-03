using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using finisher_service.api;
using finisher_service.api.model;
using finisher_service.lib;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using TechTalk.SpecFlow;

namespace finisher_service.tests.Steps
{
    [Binding]
    public sealed class CalculatorStepDefinitions
    {
        private readonly HttpClient client;
        private HttpResponseMessage response;
        private static IPersister implementationInstance = Substitute.For<IPersister>();

        public CalculatorStepDefinitions(WebApplicationFactory<Startup> factory)
        {
            client = new WebApplicationFactory<Startup>()
                            .WithWebHostBuilder(ConfigureDependencies)
                            .CreateClient();
        }

        private static void ConfigureDependencies(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton(implementationInstance);
            });
        }

        [Given("the service is initialised")]
        public void GivenTheServiceIsInitialised()
        {
        }

        [When("a FinishEvent is posted to the finsih endpoint")]
        public async Task WhenAFinishEventIsPostedAsync()
        {
            response = await client.PostAsync(
                "/finish",
                JsonContent.Create(new FinishEvent()));
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

        [Then("the finish is persisted")]
        public void AndTheFinishIsPersisted()
        {
            implementationInstance.Received(1).PersistFinishAsync();
            response.EnsureSuccessStatusCode();
        }
    }
}