using PolRegio.Domain.Models.Components;
using PolRegio.Domain.Models.Components.Contact;
using PolRegio.Domain.Models.Components.Home;
using PolRegio.Domain.Models.UmbracoCreate;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Models.View.News
{
    /// <summary>
    /// Klasa zawierająca obiekty wyświetlane na stronie ze wszystkimi informacjami
    /// </summary>
    public class NewsPageViewModel
    {
        /// <summary>
        /// Enumerator zawierający filtr regionu
        /// </summary>
        public List<FilterItem> RegionFilter { get; set; }
        /// <summary>
        /// Wybrany filtr z dropdown
        /// </summary>
        public int SelectedRegionId { get; set; }
        /// <summary>
        /// Enumerator zawierający filt z kategorią informacji
        /// </summary>
        public List<CheckBoxFilterItem> NewsTypeFilter { get; set; }
        /// <summary>
        /// Lista zawierająca boxy z informacjami wyświetlane na stronie ze wszytskimi artykułami
        /// </summary>
        public IEnumerable<NewsBoxModel> NewsBoxesList { get; set; }
        /// <summary>
        /// Liczba wszystkich dostępnych artykułów do wyświetlenia
        /// </summary>
        public int AllNewsCount { get; set; }
        /// <summary>
        /// Liczba wyświetlanych elementów po LoadMore lub po zmianie filtrów
        /// </summary>
        public int DisplayCount { get; set; }
        /// <summary>
        /// Aktualne id strony
        /// </summary>
        public int CurrentUmbracoPageId { get; set; }
        /// <summary>
        /// Culture info obecnej strony
        /// </summary>
        public CultureInfo CurrentPageCulture { get; set; }
        /// <summary>
        /// Parametr przekazany w url przy akcji w menu
        /// </summary>
        public string SelectedTypeFromUrl { get; set; }
        /// <summary>
        /// Obiekt zawierający dane kontaktowe do regionalnego oddziału
        /// Pobierany ze strony Kontakt -> Oddziały
        /// po przypisanym regionie, w przypadku wielu kontaktów wybierany jest pierwszy z brzegu
        /// </summary>
        public RegionContactBox RegionContact { get; set; }
        /// <summary>
        /// Lista zawierająca zaznaczone typy informacji
        /// </summary>
        public List<int> SelectedTypeIds { get; set; }
    }
}
