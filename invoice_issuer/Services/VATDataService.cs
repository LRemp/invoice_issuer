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
        public async Task<VATCountry?> GetCountryVATData(string countryCode)
        {
            var client = httpClientFactory.CreateClient("vatapi");
            return await client.GetFromJsonAsync<VATCountry>($"/rates/{countryCode}");
        }
    }
}
