using Newtonsoft.Json;
using PolRegio.Domain.Models.Components;
using PolRegio.Domain.Models.UmbracoCreate;
using PolRegio.Helpers.Enums;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;

namespace PolRegio.Domain.Models.View.ContractNoticeModel
{
    /// <summary>
    /// Klasa zawierająca obiekty wyświetlane na stronie szczegółów zamówienia publicznego
    /// </summary>
    public class ContractNoticeViewModel : ContractNotice
    {
        public ContractNoticeViewModel(IPublishedContent content) : base(content)
        {
        }
        /// <summary>
        /// Lista dokumentów do pobrania 
        /// </summary
        public IEnumerable<DownloadItem> DownloadDocuments { get; set; }
        /// <summary>
        /// Link do strony ze wszystkimi ogłoszeniami
        /// </summary>
        public string ParentUrl { get { return Content.Parent.Url; } }
        /// <summary>
        /// Sprzedający
        /// </summary>
        public string AdministrativeKey
        {
            get
            {
                if (this.Administrative != null)
                {
                    var _administrative = JsonConvert.DeserializeObject<IEnumerable<NuPickersSqlDropDownPicker>>(this.Administrative.SavedValue.ToString()).FirstOrDefault();
                    return _administrative.Label;
                }

                return string.Empty;
            }
        }
        /// <summary>
        /// Klucz dictionary do statusu
        /// </summary>
        public string StatusKey
        {
            get
            {
                return string.Format("Advertising.Placeholder.{0}", this.GetStatus());
            }
        }
        /// <summary>
        /// Klucz dictionary do rodzaju zamówienia
        /// </summary>
        public string TypeOfContractKey
        {
            get
            {
                if (this.TypeOfContract != null)
                {
                    var _enumItem = this.TypeOfContract.AsEnums<NoticesTypeOfContractEnum>().FirstOrDefault();
                    return string.Format("Notices.TypeOfContract.Placeholder.{0}", _enumItem);
                }
                return string.Empty;
            }
        }
        /// <summary>
        /// Klucz dictionary do aktu prawnego
        /// </summary>
        public string LawActKey
        {
            get
            {
                if (this.LawAct != null)
                {
                    var _enumItem = this.LawAct.AsEnums<NoticesLawActEnum>().FirstOrDefault();
                    return string.Format("Notices.LawAct.Placeholder.{0}", _enumItem);
                }
                return string.Empty;
            }
        }
    }
}
