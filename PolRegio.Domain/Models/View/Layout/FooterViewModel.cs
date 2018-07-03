using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolRegio.Domain.Models.Components;

namespace PolRegio.Domain.Models.View.Layout
{
    public class FooterViewModel
    {
        /// <summary>
        /// Url do strony głównej
        /// </summary>
        public string HomePageUrl { get; set; }
        /// <summary>
        /// Lista linków do dostępnych lokalizacji językowych
        /// </summary>
        public IEnumerable<Link> Languages { get; set; }
        /// <summary>
        /// Lista zawierająca elementy footer
        /// </summary>
        public List<MenuItem> MenuItems { get; set; }

        /// <summary>
        /// Lista zawierająca elementy socialMedia
        /// </summary>
        public IEnumerable<SocilaItem> SocialMediaItems { get; set; }
        /// <summary>
        /// Link do BIP
        /// </summary>
        public string BipUrl { get; set; }
        public bool BipUrlTarget { get; set; }
        public Guid? SalesManagoContactId { get; set; }

    }
}
