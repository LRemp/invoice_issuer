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
        private static async Task<IResult> HandleAsync(Request request)
        {
            throw new NotImplementedException();
        }
    }
}
