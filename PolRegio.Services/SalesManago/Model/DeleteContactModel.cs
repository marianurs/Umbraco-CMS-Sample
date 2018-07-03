namespace PolRegio.Services.SalesManago.Model
{
    internal class DeleteContactModel : BaseSalesManagoRequestModel
    {
        public string email { get; set; }
        public string owner { get; set; }
        public bool permanently { get; set; }
    }
}