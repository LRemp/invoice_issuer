namespace invoice_issuer.Entities
{
    public class VATCountry
    {
        public string CountryCode { get; set; } = string.Empty;
        public List<VATRate> Rates;
    }
}
