namespace invoice_issuer.Features
{
    public static class HttpClients
    {
        public static void AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("countryapi", client =>
            {
                client.BaseAddress = new Uri("https://https://api.first.org");
            });

            services.AddHttpClient("vatapi", client =>
            {
                client.BaseAddress = new Uri("https://api.vatlookup.eu");
            });
        }
    }
}
