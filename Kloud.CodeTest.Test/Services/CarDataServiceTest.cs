using Kloud.CodeTest.Core.Contracts.Services;
using Kloud.CodeTest.Core.Services;
using Kloud.CodeTest.Test.Shared;
using NUnit.Framework;
using System.Linq;

namespace Kloud.CodeTest.Test.Services
{
    public class CarDataServiceTest
    {
        private static readonly string JSON = "[{\"name\":\"Bradley\",\"cars\":[{\"brand\":\"MG\",\"colour\":\"Blue\"}]},{\"name\":\"Demetrios\",\"cars\":[{\"brand\":\"Toyota\",\"colour\":\"Green\"},{\"brand\":\"Holden\",\"colour\":\"Blue\"}]},{\"name\":\"Brooke\",\"cars\":[{\"brand\":\"Holden\",\"colour\":\"Red\"}]}]";

        private ICarDataService GetCarDataService()
        {
            ICarDataService service = new CarDataService(TestBase.MockCarDataProvider(JSON));
            return service;
        }

        [Test]
        public void Validate_BrandCount()
        {
            var data = GetCarDataService().GetOwnerNamesByBrandAsync().Result;
            Assert.AreEqual(data.Count, 3);
        }

        [Test]
        public void Validate_OwnersCount_By_Brand()
        {
            var data = GetCarDataService().GetOwnerNamesByBrandAsync().Result;
            Assert.AreEqual(data.Where(e => e.Brand == "Holden").FirstOrDefault().Owners.Count, 2);
        }

        [Test]
        public void Validate_SortOrder_By_Color()
        {
            var data = GetCarDataService().GetOwnerNamesByBrandAsync().Result;
            Assert.Greater(data.Where(e => e.Brand == "Holden").FirstOrDefault().Owners[0],
                data.Where(e => e.Brand == "Holden").FirstOrDefault().Owners[1]);
        }
    }
}