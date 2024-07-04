using invoice_issuer.Contracts;
using invoice_issuer.Domain;
using invoice_issuer.Entities;

namespace invoice_issuer.Features.Invoices.Calculate
{
    public static class Endpoint
    {
        /// <summary>
        /// Map API endpoint to the web application
        /// </summary>
        /// <param name="app">Web application object</param>
        /// <returns>Web application object with the mapped endpoint</returns>
        public static void MapInvoiceCalculateEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("api/invoices/calculate", HandleAsync);
        }

        /// <summary>
        /// Asynchronous endpoint handler
        /// </summary>
        /// <param name="request">Request object</param>
        /// <returns>Response of calculated invoice</returns>
        public static async Task<IResult> HandleAsync(Request request, ICountryDataService countryDataService, IVATDataService vatDataService)
        {
            //If service provider is not VAT payer, do not apply VAT tax to the final price
            if(!request.Merchant.VATpayer)
            {
                return Results.Ok(new Response
                {
                    Customer = request.Customer,
                    Merchant = request.Merchant,
                    Price = request.Price,
                    VATratio = 0,
                    TotalPrice = request.Price
                });
            }

            //Fetch data of customer and merchant country
            var customerCountry = await countryDataService.GetCountryData(request.Customer.Country);

            if (customerCountry is null)
            {
                return Results.Problem(DomainErrors.Invoices.CustomerCountryNotFound.Code);
            }

            var merchantCountry = await countryDataService.GetCountryData(request.Merchant.Country);

            if (merchantCountry is null)
            {
                return Results.Problem(DomainErrors.Invoices.MerchantCountryNotFound.Code);
            }

            if (!customerCountry.Region.Equals("Europe"))
            {
                return Results.Ok(new Response
                {
                    Customer = request.Customer,
                    Merchant = request.Merchant,
                    Price = request.Price,
                    VATratio = 0,
                    TotalPrice = request.Price
                });
            }

            //Fetch data of VAT taxes applied in customer and merchant countries
            var customerVATdata = await vatDataService.GetCountryVATData(request.Customer.Country, "Standard");

            if(customerVATdata is null)
            {
                return Results.Problem(DomainErrors.Invoices.VATdataNotFound.Code);
            }

            if(customerVATdata.Rates.Count == 0)
            {
                return Results.Ok(new Response
                {
                    Customer = request.Customer,
                    Merchant = request.Merchant,
                    Price = request.Price,
                    VATratio = 0,
                    TotalPrice = request.Price
                });
            }

            double VATratio =  1.0 + (double)customerVATdata.Rates[0] / 100;

            return Results.Ok(new Response
            {
                Customer = request.Customer,
                Merchant = request.Merchant,
                Price = request.Price,
                VATratio = customerVATdata.Rates[0],
                TotalPrice = request.Price * (decimal)VATratio
            });
        }
    }
}
