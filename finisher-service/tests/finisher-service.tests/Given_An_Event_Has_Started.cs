using System.Net.Http.Json;
using finisher_service.api;
using finisher_service.api.model;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace finisher_service.tests
{
    public abstract class ApiTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        protected readonly WebApplicationFactory<Startup> factory;

        protected ApiTest(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }
    }

    public class Given_An_Event_Has_Started : ApiTest
    {
        public Given_An_Event_Has_Started(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async void When_HealthCheck_Is_Called_Response_Is_Success()
        {
            var client = factory.CreateClient();

            var response = await client.GetAsync("/healthz");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async void When_Finisher_Arrives_Them_Response_Is_Success()
        {
            var client = factory.CreateClient();

            var finishEvent = new FinishEvent();

            var response = await client.PostAsync(
                "/finish", 
                JsonContent.Create(finishEvent));

            response.EnsureSuccessStatusCode();
        }
    }
}
