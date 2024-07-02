using static System.Runtime.InteropServices.JavaScript.JSType;

namespace invoice_issuer.Entities
{
    public class Rate
    {
        public string Name { get; set; }
        public List<int> Rates { get; set; }
    }
}
