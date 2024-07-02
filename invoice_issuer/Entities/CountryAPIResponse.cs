namespace invoice_issuer.Entities
{
    public class CountryAPIResponse
    {
        public string Status { get; set; }
        public int StatusCode { get; set; }
        public string Version { get; set; }
        public string Access {  get; set; }
        public int Total { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public Dictionary<string, CountryAPIResponseCountry> Data { get; set; }
    }

    public class CountryAPIResponseCountry
    {
        public string Country { get; set; }
        public string Region { get; set; }
    }
}
