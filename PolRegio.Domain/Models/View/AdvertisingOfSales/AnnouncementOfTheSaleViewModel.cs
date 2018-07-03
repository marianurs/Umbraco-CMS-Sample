using Newtonsoft.Json;
using PolRegio.Domain.Models.Components;
using PolRegio.Domain.Models.UmbracoCreate;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace PolRegio.Domain.Models.View.AdvertisingOfSalesModel
{
    /// <summary>
    /// Klasa zawierająca elementy wyświetlane na stronie szczegółów ogłoszenia o sprzedaży
    /// </summary>
    public class AnnouncementOfTheSaleViewModel : AnnouncementOfSale
    {
        public AnnouncementOfTheSaleViewModel(IPublishedContent content) : base(content)
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
    }
}
