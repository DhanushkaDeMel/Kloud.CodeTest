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
        /// <returns>List of OwnerNamesByBrand</returns>
        Task<IList<CarDataDto>> GetOwnerNamesByBrandAsync();
    }
}