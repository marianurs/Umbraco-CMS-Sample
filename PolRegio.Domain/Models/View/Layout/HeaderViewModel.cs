using PolRegio.Domain.Models.Components;
using PolRegio.Domain.Models.UmbracoCreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using PolRegio.Domain.Models.View.Home;

namespace PolRegio.Domain.Models.View.Layout
{
    /// <summary>
    /// Klasa zawierająca obiekty headera
    /// </summary>
    public class HeaderViewModel
    {
        /// <summary>
        /// Url do strony głównej
        /// </summary>
        public string HomePageUrl { get; set; }
        /// <summary>
        /// Lista linków do dostępnych lokalizacji językowych
        /// </summary>
        public IEnumerable<LangLink> Languages { get; set; }
        /// <summary>
        /// Lista zawierająca elementy menu
        /// </summary>
        public List<MenuItem> MenuItems { get; set; }
        /// <summary>
        /// Numer telefonu do infolinii
        /// </summary>
        public string HelplineNumber { get; set; }
        /// <summary>
        /// Tekst tooltipa w infolinii
        /// </summary>
        public IHtmlString HelplineTooltip { get; set; }
        /// <summary>
        /// Informacja o ciasteczkach
        /// </summary>
        public Cookies CookieInformation { get; set; }

        public HeaderAlertViewModel Alert { get; set; }
        public OverlayViewModel Overlay { get; set; }
    }
}
