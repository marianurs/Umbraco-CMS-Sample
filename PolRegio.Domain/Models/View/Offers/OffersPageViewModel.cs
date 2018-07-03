using PolRegio.Domain.Models.Components.Offers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Models.View.Offers
{
    /// <summary>
    /// Klasa zawierająca obiekty wyświetlane na stronie oferty i promocje
    /// </summary>
    public class OffersPageViewModel
    {
        /// <summary>
        /// Lista obiektów do wyświetlenia na stronie
        /// </summary>
        public string OffersPageName { get; set; }
        /// <summary>
        /// Lista obiektów do wyświetlenia na stronie
        /// </summary>
        public List<OfferItem> OffersBox { get; set; }
    }
}
