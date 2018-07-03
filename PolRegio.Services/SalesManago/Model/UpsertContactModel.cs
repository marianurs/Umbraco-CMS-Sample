using System;
using System.Collections.Generic;

namespace PolRegio.Services.SalesManago.Model
{
    internal class UpsertContactModel : BaseSalesManagoRequestModel
    {
        public UpsertContactModel()
        {
            tags = new List<string>();
        }

        public bool async { get; set; }
        public ContactModel contact { get; set; }
        public string owner { get; set; }
        public string newEmail { get; set; }
        public string forceOptIn { get; set; }
        public string forceOptOut { get; set; }
        public string forcePhoneOptIn { get; set; }
        public string forcePhoneOptOut { get; set; }
        public List<string> tags { get; set; }
        public List<string> removeTags { get; set; }
        public ContactCustomProperties properties { get; set; }
        public string useApiDoubleOptIn { get; set; }
        public string lang { get; set; }
    }
}