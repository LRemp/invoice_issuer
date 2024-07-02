using invoice_issuer.Enums;

namespace invoice_issuer.Entities
{
    public class Client
    {
        public string Country { get; set; }
        public string Name { get; set; }
        public Classification Type { get; set; }
    }
}
