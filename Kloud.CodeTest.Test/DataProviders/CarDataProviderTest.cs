using Kloud.CodeTest.Core.Contracts.DataProviders;
using Kloud.CodeTest.Core.DataProviders;
using Kloud.CodeTest.Test.Shared;
using NUnit.Framework;
using System.Net;

namespace Kloud.CodeTest.Test.DataProviders
{
    public class CarDataProviderTest
    {
        [Test]
        public void When_ServerReturn_Error()
        {
            var client = TestBase.MockHttpClient(HttpStatusCode.InternalServerError, string.Empty);

            ICarDataProvider provider = new CarDataProvider(client, TestBase.MockAppSettings());
            var data = provider.GetAsync().Result;

            Assert.IsNull(data);
        }

        [Test]
        public void When_ServerReturn_Success_Empty()
        {
            var client = TestBase.MockHttpClient(HttpStatusCode.OK, "[]");

            ICarDataProvider provider = new CarDataProvider(client, TestBase.MockAppSettings());
            var data = provider.GetAsync().Result;

            Assert.AreEqual(data.Count, 0);
        }

        [Test]
        public void When_ServerReturn_Success_Data()
        {
            string json = "[{\"name\":\"Bradley\",\"cars\":[{\"brand\":\"MG\",\"colour\":\"Blue\"}]}]";
            var client = TestBase.MockHttpClient(HttpStatusCode.OK, json);

            ICarDataProvider provider = new CarDataProvider(client, TestBase.MockAppSettings());
            var data = provider.GetAsync().Result;

            Assert.AreEqual(1, data.Count);
        }

        [Test]
        public void When_ServerReturn_Success_InvalidJson()
        {
            string json = "[{\"id\":\"10001\"}]";
            var client = TestBase.MockHttpClient(HttpStatusCode.OK, json);

            ICarDataProvider provider = new CarDataProvider(client, TestBase.MockAppSettings());
            var data = provider.GetAsync().Result;

            Assert.IsNull(data[0].Name);
        }
    }
}