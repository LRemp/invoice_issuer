using invoice_issuer.Entities;

namespace invoice_issuer.Contracts
{
    public interface IVATDataService
    {
        public Task<Rate?> GetCountryVATData(string countryCode, string type);
    }
}
