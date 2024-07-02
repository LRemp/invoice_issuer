using System.Diagnostics.Metrics;
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
        public async Task<Country?> GetCountryData(string countryCode)
        {
            CountryAPIResponse? response = null;

            Country? country = null;

            while(country is null)
            {
                response = await GetAPIResponse(response);

                //Break the loop if API does not respond
                if(response is null)
                {
                    break;
                }

                //Searching for country data in dictionary based on country code as a key
                foreach(string item in response.Data.Keys)
                {
                    if(item.ToLower().Equals(countryCode.ToLower()))
                    {
                        country = new Country
                        {
                            Code = item.ToLower(),
                            Name = response.Data[item].Country,
                            Region = response.Data[item].Region,
                        };
                    }
                }

                //End data fetching if the data elements limit reached
                if (response.Offset + response.Limit > response.Total)
                {
                    break;
                }
            }

            return country;
        }
        
        /// <summary>
        /// Query country API response based on API pagination
        /// </summary>
        /// <param name="data">Previous API page response</param>
        /// <returns>Next API page response</returns>
        private async Task<CountryAPIResponse?> GetAPIResponse(CountryAPIResponse? data)
        {
            var client = httpClientFactory.CreateClient("countryapi");

            string query = "/data/v1/countries";
            if(data != null)
            {
                query += $"?offset={data.Offset + data.Limit}";
            }

            var result = await client.GetFromJsonAsync<CountryAPIResponse>(query);

            return result;
        }
    }
}
