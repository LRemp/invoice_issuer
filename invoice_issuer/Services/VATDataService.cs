using invoice_issuer.Contracts;
using invoice_issuer.Entities;

namespace invoice_issuer.Services
{
    /// <summary>
    /// Service for fetching VAT data
    /// </summary>
    public class VATDataService : IVATDataService
    {
        private readonly IHttpClientFactory httpClientFactory;
        public VATDataService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Get country VAT data
        /// </summary>
        /// <param name="countryCode">Country code</param>
        /// <returns>Country VAT data</returns>
        public Task<VATCountry> GetCountryVATData(string countryCode)
        {
            throw new NotImplementedException();
        }
    }
}
