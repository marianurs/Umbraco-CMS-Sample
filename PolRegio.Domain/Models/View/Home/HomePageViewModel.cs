using PolRegio.Domain.Models.Components.Home;
using PolRegio.Domain.Models.UmbracoCreate;
using PolRegio.Domain.Models.View.SearchTicket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolRegio.Domain.Models.View.Layout;

namespace PolRegio.Domain.Models.View.Home
{
    /// <summary>
    /// Klasa zawierająca obiekty wyświetlane na stronie głównej
    /// </summary>
    public class HomePageViewModel
    {
        /// <summary>
        /// Obiekt strony głównej pobrany z CMS
        /// </summary>
        public Location HomePageFromDb { get; set; }
        /// <summary>
        /// Lista boxów z ofertami i promocjami
        /// </summary>
        public IEnumerable<OfferBoxModel> OffersListDisplayOnHomePage { get; set; }
        /// <summary>
        /// Ilość ofert do wyświetlenia na home page
        /// </summary>
        public int OffersListCount { get { return OffersListDisplayOnHomePage.Count(); } }
        /// <summary>
        /// Lista boxów z aktualnościami
        /// </summary>
        public IEnumerable<NewsBoxModel> NewsListDisplayOnHomePage { get; set; }
        /// <summary>
        /// Ilość newsów do wyświetlenia na home page
        /// </summary>
        public int NewsListCount { get { return OffersListDisplayOnHomePage.Count(); } }
        /// <summary>
        /// Link do strony ze wszytskimi ofertami i promocjami
        /// </summary>
        public string AllOffersUrl { get; set; }
        /// <summary>
        /// Link do strony ze wszystkimi informacjami
        /// </summary>
        public string AllNewsUrl { get; set; }
        /// <summary>
        /// Model do wyszukiwarki połączeń
        /// </summary>
        public SearchTicketFormView SearchTicketModel { get; set; }

        public BanerViewModel Baner { get; set; }

        public IEnumerable<SliderItem> SliderSlides { get; set; }
        
    }
}
