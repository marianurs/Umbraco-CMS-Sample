using System.Collections.Generic;

namespace PolRegio.Services.SalesManago.Model
{
    internal class GetContactModel : BaseSalesManagoRequestModel
    {
        public GetContactModel()
        {
            contactId = new List<string>();
        }

        public List<string> contactId { get; set; }
        public string owner { get; set; }
    }
}