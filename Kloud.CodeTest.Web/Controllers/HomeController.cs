using AutoMapper;
using Kloud.CodeTest.Core.Contracts.Services;
using Kloud.CodeTest.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace Kloud.CodeTest.Web.Controllers
{
    /// <summary>
    /// HomeController Class
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICarDataService _carDataService;

        /// <summary>
        ///  Default Constructor
        /// </summary>
        /// <param name="mapper">Instance of Type IMapper</param>
        /// <param name="carDataService">Instance of Type ICarDataService</param>
        public HomeController(IMapper mapper, ICarDataService carDataService)
        {
            _mapper = mapper;
            _carDataService = carDataService;
        }

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var result = _carDataService.GetOwnerNamesByBrandAsync().Result;
            var model = _mapper.Map<IList<HomeViewModel>>(result);

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}