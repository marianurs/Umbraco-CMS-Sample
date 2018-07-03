namespace PolRegio.Services.SalesManago.Model
{
    internal class BaseSalesManagoRequestModel
    {
        public string apiKey { get; set; }
        public string clientId { get; set; }
        public string sha { get; set; }
        public int requestTime { get; set; }
    }
}