using Kloud.CodeTest.Core.Configurations;
using Kloud.CodeTest.Core.Contracts.DataProviders;
using Kloud.CodeTest.Core.Entities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Kloud.CodeTest.Core.DataProviders
{
    /// <summary>
    /// CarDataProvider Class
    /// </summary>
    public class CarDataProvider : ICarDataProvider
    {
        private const string API_ENDPOINT = "api/cars";

        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CarDataProvider(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Get JSON Data
        /// </summary>
        /// <returns>List of Owner</returns>
        public async Task<IList<Owner>> GetAsync()
        {
            IList<Owner> result = null;

            try
            {
                var content = await _httpClient.GetStringAsync(API_ENDPOINT);
                result = JsonConvert.DeserializeObject<IList<Owner>>(content);
            }
            catch
            {
                return result;
            }

            return result;
        }
    }
}