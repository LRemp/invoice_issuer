using invoice_issuer.Contracts;

namespace invoice_issuer.Services
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICountryDataService, CountryDataService>();
            services.AddTransient<IVATDataService, VATDataService>();
        }
    }
}
