using static System.Runtime.InteropServices.JavaScript.JSType;

namespace invoice_issuer.Entities
{
    public class VATCountry
    {
        public List<Rate> Rates { get; set; }
        public string Disclaimer { get; set; }
    }
}
