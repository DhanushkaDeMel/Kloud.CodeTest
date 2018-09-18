using Kloud.CodeTest.Core.Configurations;
using Kloud.CodeTest.Core.Contracts.DataProviders;
using Kloud.CodeTest.Core.DataProviders;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using RichardSzalay.MockHttp;
using System;
using System.Net;
using System.Net.Http;

namespace Kloud.CodeTest.Test.DataProviders
{
    public class CarDataProviderTest
    {
        private readonly IOptions<AppSettings> appSettings;

        public CarDataProviderTest()
        {
            appSettings = Options.Create(new AppSettings() { WebServiceUrl = "http://localhost" });
        }

        private HttpClient MockHttpClient(HttpStatusCode httpStatusCode, string json)
        {
            var mockHttp = new MockHttpMessageHandler();
            var mockRequest = mockHttp.When("http://localhost/api/cars");

            if (!string.IsNullOrEmpty(json))
            {
                mockRequest.Respond(httpStatusCode, "application/json", json);
            }
            else
            {
                mockRequest.Respond(httpStatusCode);
            }
            var client = mockHttp.ToHttpClient();
            client.BaseAddress = new Uri("http://localhost");
            return client;
        }

        [Test]
        public void WhenServerReturn_Error()
        {
            var client = MockHttpClient(HttpStatusCode.InternalServerError, string.Empty);

            ICarDataProvider service = new CarDataProvider(client, appSettings);
            var data = service.GetAsync();

            Assert.IsNull(data.Result);
        }

        [Test]
        public void WhenServerReturn_Success_Empty()
        {
            var client = MockHttpClient(HttpStatusCode.OK, "[]");

            ICarDataProvider service = new CarDataProvider(client, appSettings);
            var data = service.GetAsync();

            Assert.AreEqual(data.Result.Count, 0);
        }

        [Test]
        public void WhenServerReturn_Success_1_Item()
        {
            string json = "[{\"name\":\"Bradley\",\"cars\":[{\"brand\":\"MG\",\"colour\":\"Blue\"}]}]";
            var client = MockHttpClient(HttpStatusCode.OK, json);

            ICarDataProvider service = new CarDataProvider(client, appSettings);
            var data = service.GetAsync();

            Assert.AreEqual(1, data.Result.Count);
        }

        [Test]
        public void WhenServerReturn_Success_InvalidJson()
        {
            string json = "[{\"id\":\"10001\"}]";
            var client = MockHttpClient(HttpStatusCode.OK, json);

            ICarDataProvider service = new CarDataProvider(client, appSettings);
            var data = service.GetAsync();

            Assert.IsNull(data.Result[0].Name);
        }
    }
}