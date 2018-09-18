using Kloud.CodeTest.Core.Configurations;
using Kloud.CodeTest.Core.Contracts.DataProviders;
using Kloud.CodeTest.Core.DataProviders;
using Microsoft.Extensions.Options;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Kloud.CodeTest.Test.DataProviders
{
    public class CarDataProviderTest
    {
        readonly IOptions<AppSettings> appSettings;

        public CarDataProviderTest()
        {
            appSettings = Options.Create(new AppSettings() { WebServiceUrl = "http://localhost" });
        }

        //private HttpClient MockHttpClient(string successJson, HttpStatusCode errorCode = HttpStatusCode.OK, Action<MockHttpMessageHandler> handle = null)
        //{
        //    var mockHttp = new MockHttpMessageHandler();
        //    var request = mockHttp.When("http://localhost/api/cars");

        //    if (!string.IsNullOrEmpty(successJson))
        //    {
        //        request.Respond("application/json", successJson);
        //    }
        //    else
        //    {
        //        if (errorCode > 0)
        //        {
        //            request.Respond(errorCode, );
        //        }
        //        else
        //            handle?.Invoke(mockHttp);
        //    }
        //    var client = mockHttp.ToHttpClient();
        //    client.BaseAddress = new Uri("http://localhost");
        //    return client;
        //}

        [Fact]
        public void WhenServerError()
        {
            var mockHttp = new MockHttpMessageHandler();
            var request = mockHttp.When("http://localhost/api/cars");
            request.Respond(HttpStatusCode.InternalServerError, new StringContent(""));
            var client = mockHttp.ToHttpClient();
            client.BaseAddress = new Uri("http://localhost");

            ICarDataProvider service = new CarDataProvider(client, appSettings);

            var results = service.GetAsync();

            Assert.Empty(results.Result);

        }



        //[Fact]
        //public void GetData_ShouldReturn_2Items()
        //{
        //    string json = "[{\"name\":\"Bradley\",\"cars\":[{\"brand\":\"MG\",\"colour\":\"Blue\"}]},{\"name\":\"Marry\",\"cars\":[{\"brand\":\"Toyota\",\"colour\":\"Blue\"}]}]";
        //    var httpClient = MockHttpClient(json);

        //    ICarDataProvider service = new CarDataProvider(httpClient, settingConfig, mockLogger.Object);
        //    var results = service.GetData();

        //    Assert.Equal(2, results.Count);

        //}


        //[Fact]
        //public void GetData_ShouldReturn_ZeroItems_WhenServiceResponseEmpty()
        //{

        //    string json = "[]";
        //    var httpClient = MockHttpClient(json);
        //    ICarDataProvider service = new CarDataProvider(httpClient, settingConfig, mockLogger.Object);
        //    var results = service.GetData();
        //    Assert.Empty(results);

        //}

        //[Fact]
        //public void GetData_ShouldNotCrash_WhenServiceResponseInvalidFormat()
        //{
        //    string json = "[{\"abc\": \"xyz\"}]";
        //    var httpClient = MockHttpClient(json);
        //    ICarDataProvider service = new CarDataProvider(httpClient, settingConfig, mockLogger.Object);
        //    var results = service.GetData();

        //    Assert.Empty(results);
        //}

        //[Fact]
        //public void GetData_ShouldNotCrash_WhenServiceResponseNotJSONValid()
        //{
        //    string json = "random string here.....";
        //    var httpClient = MockHttpClient(json);
        //    ICarDataProvider service = new CarDataProvider(httpClient, settingConfig, mockLogger.Object);
        //    var results = service.GetData();

        //    Assert.Empty(results);
        //}

        //}
    }
}
