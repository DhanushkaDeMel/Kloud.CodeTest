using Kloud.CodeTest.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kloud.CodeTest.Core.Contracts.DataProviders
{
    /// <summary>
    /// IJosnDataProvider Interface
    /// </summary>
    public interface ICarDataProvider
    {
        /// <summary>
        /// Get JSON Data
        /// </summary>
        /// <returns>List of Owner</returns>
        Task<IList<Owner>> GetAsync();
    }
}