using Kloud.CodeTest.Core.Configurations;
using Kloud.CodeTest.Core.Contracts.DataProviders;
using Kloud.CodeTest.Core.DataProviders;
using Microsoft.Extensions.Options;
using RichardSzalay.MockHttp;
using System;
using System.Net;
using System.Net.Http;

namespace Kloud.CodeTest.Test.Shared
{
    public static class TestBase
    {
        public static IOptions<AppSettings> MockAppSettings()
        {
            var appSettings = Options.Create(new AppSettings() { WebServiceUrl = "http://localhost" });
            return appSettings;
        }

        public static HttpClient MockHttpClient(HttpStatusCode httpStatusCode, string json)
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

        public static ICarDataProvider MockCarDataProvider(string json)
        {
            var client = MockHttpClient(HttpStatusCode.OK, json);
            return new CarDataProvider(client, MockAppSettings());
        }
    }
}