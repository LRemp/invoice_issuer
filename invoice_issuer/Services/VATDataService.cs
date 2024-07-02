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
        public async Task<Rate?> GetCountryVATData(string countryCode, string type)
        {
            var client = httpClientFactory.CreateClient("vatapi");

            var response = await client.GetFromJsonAsync<VATCountry>($"/rates/{countryCode}");

            var test = await client.GetAsync($"/rates/{countryCode}");

            if (response is null)
            {
                return null;
            }

            return response.Rates.Where(x => x.Name == type).FirstOrDefault();
        }
    }
}
