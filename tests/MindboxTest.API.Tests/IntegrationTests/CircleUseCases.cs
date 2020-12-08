using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using MindboxTest.API.Responses;
using MindboxTest.Handlers.AddFigure;
using MindboxTest.Handlers.CalcArea;
using MindboxTest.TestHelpers;

using Newtonsoft.Json;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MindboxTest.API.Tests.IntegrationTests
{
    public class CircleUseCases
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
        public async Task AddRightCircle_CalculateIt_RightArea()
        {
            var client = _host.GetTestClient();

            ApiResponse<AddFigureResponseData> addCircleResult = await AddCircle(client, 1); 

            Assert.AreEqual(0, addCircleResult.Errors.Count);

            long figureId = addCircleResult.Data.FigureId;

            HttpResponseMessage response = await client.GetAsync($"/figure/{figureId}");

            string responseString = await response.Content.ReadAsStringAsync();

            ApiResponse<CalcAreaResponseData> result = JsonConvert.DeserializeObject<ApiResponse<CalcAreaResponseData>>(responseString);

            Assert.AreEqual(0, result.Errors.Count);

            Assert.That(result.Data.Area, Is.EqualTo(Math.PI).Within(DoubleHelpers.Tolerance));
        }

        [Test]
        public async Task AddWrongCircle_ValidationError()
        {
            var client = _host.GetTestClient();

            ApiResponse<AddFigureResponseData> addCircleResult = await AddCircle(client, -1);

            Assert.AreEqual(1, addCircleResult.Errors.Count);
        }

        private static async Task<ApiResponse<AddFigureResponseData>> AddCircle(HttpClient client, double radius)
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/figure");
            StringContent jsonContent = new StringContent(
            @$"
                {{
                    type:'circle',
                    description: {{radius: {radius}}}
                }}
            ");

            postRequest.Content = jsonContent;

            HttpResponseMessage response = await client.SendAsync(postRequest);

            string responseString = await response.Content.ReadAsStringAsync();

            ApiResponse<AddFigureResponseData> result = JsonConvert.DeserializeObject<ApiResponse<AddFigureResponseData>>(responseString);

            return result;
        }
    }
}