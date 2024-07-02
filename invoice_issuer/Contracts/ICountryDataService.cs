using invoice_issuer.Entities;

namespace invoice_issuer.Contracts
{
    public interface ICountryDataService
    {
        public Task<Country?> GetCountryData(string countryCode);
    }
}
