using PolRegio.Domain.Models.UmbracoCreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace PolRegio.Domain.Models.Components.Offers
{
    /// <summary>
    /// Klasa zawierająca obiekt wyświetlany na stronie oferty i promocje
    /// </summary>
    public class OfferItem
    {
        /// <summary>
        /// Nazwa strony
        /// </summary>
        public string offersPageName { get; set; }
        /// <summary>
        /// Link do strony
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// Kategoria strony
        /// </summary>
        public string pageCategory { get; set; }
        
    }
}
