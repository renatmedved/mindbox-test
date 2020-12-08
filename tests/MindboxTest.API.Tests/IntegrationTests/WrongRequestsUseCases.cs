using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using MindboxTest.API.Responses;
using MindboxTest.Handlers.AddFigure;

using Newtonsoft.Json;

using NUnit.Framework;

using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MindboxTest.API.Tests.IntegrationTests
{
    public class WrongRequestsUseCases
    {
        private IHost _host;

        [SetUp]
        public async Task StartServer()
        {
            var myConfiguration = new Dictionary<string, string>
            {
                {"ConnectionStrings:figures", "DB/figures.sqlite"},
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    webHost.UseTestServer();
                    webHost.UseEnvironment("Development");
                    webHost.UseConfiguration(configuration);

                    webHost.UseStartup<Startup>();
                });

            _host = await hostBuilder.StartAsync();
        }

        [Test]
        public async Task AddFigureEmptyRequest()
        {
            HttpClient client = _host.GetTestClient();

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/figure");
            StringContent jsonContent = new StringContent("{}");

            postRequest.Content = jsonContent;

            HttpResponseMessage response = await client.SendAsync(postRequest);

            string responseString = await response.Content.ReadAsStringAsync();

            ApiResponse<AddFigureResponseData> result = JsonConvert.DeserializeObject<ApiResponse<AddFigureResponseData>>(responseString);

            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual("error figure", result.Errors.Single());
        }

        [Test]
        public async Task CalculateNotExistFigure()
        {
            HttpClient client = _host.GetTestClient();

            HttpResponseMessage response = await client.GetAsync("/figure/-1");

            string responseString = await response.Content.ReadAsStringAsync();

            ApiResponse<AddFigureResponseData> result = JsonConvert.DeserializeObject<ApiResponse<AddFigureResponseData>>(responseString);

            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual("figure not found", result.Errors.Single());
        }

    }
}