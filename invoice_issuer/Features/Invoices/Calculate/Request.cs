using invoice_issuer.Entities;

namespace invoice_issuer.Features.Invoices.Calculate
{
    public record Request
    {
        public Client Customer { get; set; }
        public Provider Merchant { get; set; }
        public decimal Price { get; set; }
    }
}
