using System.Collections.Generic;

namespace PolRegio.Services.SalesManago.Model
{
    internal class AddContactModel : BaseSalesManagoRequestModel
    {
        public AddContactModel()
        {
            tags = new List<string>();
        }

        public string owner { get; set; }
        public ContactModel contact { get; set; }
        public string forceOpOut { get; set; }
        public string forcePhoneOptOut { get; set; }
        public IList<string> tags { get; set; }
        public ContactCustomProperties properties { get; set; }
        public string useApiDoubleOptIn { get; set; }
        public string lang { get; set; }
    }
}