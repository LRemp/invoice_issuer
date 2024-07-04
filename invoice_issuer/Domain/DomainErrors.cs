using invoice_issuer.Helpers;

namespace invoice_issuer.Domain
{
    public static class DomainErrors
    {
        public static class Invoices
        {
            public static readonly Error CustomerCountryNotFound = new Error(
                "Invoice.CustomerCountryNotFound",
                "Customer country not found");

            public static readonly Error MerchantCountryNotFound = new Error(
                "Invoice.MerchantCountryNotFound",
                "Merchant country not found");

            public static readonly Error VATdataNotFound = new Error(
                "Invoice.VATdataNotFound",
                "VAT country tax data not found");
        }
    }
}
