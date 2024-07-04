namespace invoice_issuer.Helpers
{
    public class Error
    {
        public string Code { get; private set; }
        public string Description { get; private set; }

        public Error(string code, string description) 
        {
            this.Code = code;
            this.Description = description;
        }
    }
}
