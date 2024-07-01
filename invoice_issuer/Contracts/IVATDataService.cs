using invoice_issuer.Entities;

namespace invoice_issuer.Contracts
{
    public interface IVATDataService
    {
        public Task<VATCountry?> GetCountryVATData(string countryCode);
    }
}
