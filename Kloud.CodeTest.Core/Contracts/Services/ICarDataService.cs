using Kloud.CodeTest.Core.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kloud.CodeTest.Core.Contracts.Services
{
    /// <summary>
    /// ICarService Interface
    /// </summary>
    public interface ICarDataService
    {
        /// <summary>
        /// Get Owner Names By Brand Async
        /// </summary>
        /// <returns>Enumerable of OwnerNamesByBrand</returns>
        Task<IEnumerable<CarDataDto>> GetOwnerNamesByBrandAsync();
    }
}