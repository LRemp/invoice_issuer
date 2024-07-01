using invoice_issuer.Contracts;
using invoice_issuer.Entities;

namespace invoice_issuer.Services
{
    /// <summary>
    /// Service for gathering country data using country api
    /// </summary>
    public class CountryDataService : ICountryDataService
    {
        private readonly IHttpClientFactory httpClientFactory;
        public CountryDataService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Get country data from api
        /// </summary>
        /// <param name="countryCode">Country code</param>
        /// <returns>Country data</returns>
        public async Task<Country> GetCountryData(string countryCode)
        {
            throw new NotImplementedException();
        }
    }
}
