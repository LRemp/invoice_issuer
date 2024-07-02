using invoice_issuer.Entities;

namespace invoice_issuer.Features.Invoices.Calculate
{
    public record Response
    {
        public Client Customer { get; set; }
        public Provider Merchant { get; set; }
        public decimal Price { get; set; }
        public int VATratio { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
