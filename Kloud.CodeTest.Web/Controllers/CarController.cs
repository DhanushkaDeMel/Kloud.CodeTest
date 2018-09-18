using Kloud.CodeTest.Core.Contracts.Services;
using Kloud.CodeTest.Core.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kloud.CodeTest.Web.Controllers
{
    /// <summary>
    /// CarController Class
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarDataService _carDataService;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="carDataService">Instance of Type ICarDataService</param>
        public CarController(ICarDataService carDataService)
        {
            _carDataService = carDataService;
        }

        /// <summary>
        /// Get Owner Names By Brand
        /// </summary>
        /// <returns>Enumerable of OwnerNamesByBrand</returns>
        [HttpGet]
        public async Task<IEnumerable<CarDataDto>> Get()
        {
            var result = await _carDataService.GetOwnerNamesByBrandAsync();
            return result;
        }
    }
}