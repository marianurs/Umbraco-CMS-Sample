using PolRegio.Domain.Models.Components.BIP;
using PolRegio.Domain.Models.Components.BipPage;
using PolRegio.Domain.Models.UmbracoCreate;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Models.View.BipPage
{
    /// <summary>
    /// Klasa zawierająca elementy wyświetlane na stronie BIP
    /// </summary>
    public class BIPPageViewModel
    {
        public BIPPageViewModel()
        {
            ItemList = new List<BipListItemModel>();
        }
        /// <summary>
        /// Wyszukiwana fraza
        /// </summary>
        public string SearchText { get; set; }
        /// <summary>
        /// Elementy wyświetlane na liście 
        /// </summary>
        public List<BipListItemModel> ItemList { get; set; }
        /// <summary>
        /// Aktualne id strony
        /// </summary>
        public int CurrentUmbracoPageId { get; set; }
        /// <summary>
        /// Culture info obecnej strony
        /// </summary>
        public CultureInfo CurrentPageCulture { get; set; }
    }
}
