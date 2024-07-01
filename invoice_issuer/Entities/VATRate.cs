namespace invoice_issuer.Entities
{
    public class VATRate
    {
        public string Name { get; set; } = string.Empty;
        public int[] Rates { get; set; } = new int[0];
        public string? Disclaimer { get; set; }
    }
}
