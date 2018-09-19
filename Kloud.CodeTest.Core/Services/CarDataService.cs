using Kloud.CodeTest.Core.Contracts.DataProviders;
using Kloud.CodeTest.Core.Contracts.Services;
using Kloud.CodeTest.Core.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kloud.CodeTest.Core.Services
{
    /// <summary>
    /// CarService Class
    /// </summary>
    public class CarDataService : ICarDataService
    {
        private ICarDataProvider _carDataProvider;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CarDataService(ICarDataProvider carDataProvider)
        {
            _carDataProvider = carDataProvider;
        }

        /// <summary>
        /// Get Owner Names By Brand Async
        /// </summary>
        /// <returns>List of OwnerNamesByBrand</returns>
        public async Task<IList<CarDataDto>> GetOwnerNamesByBrandAsync()
        {
            var data = await _carDataProvider.GetAsync();

            var results = (from d in data
                           from c in d.Cars
                           group d by c.Brand
                           into o
                           orderby o.Key
                           select new CarDataDto { Brand = o.Key }).ToList();

            if (results != null)
            {
                for (int index = 0; index < results.Count; index++)
                {
                    var nestedResults = (from d in data
                                         from c in d.Cars
                                         where !string.IsNullOrEmpty(d.Name) && c.Brand == results[index].Brand
                                         select new OwnerDataDto { Owners = d.Name, Colour = c.Colour }).ToList();

                    results[index].OwnerData = nestedResults;
                }
            }

            return results;
        }
    }
}