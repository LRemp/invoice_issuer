using invoice_issuer.Features.Invoices.Calculate;

namespace invoice_issuer.Features
{
    public static class Extensions
    {
        public static void MapEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapInvoiceCalculateEndpoint();
        }
    }
}
