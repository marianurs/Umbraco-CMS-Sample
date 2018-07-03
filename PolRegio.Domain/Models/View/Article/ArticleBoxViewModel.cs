using PolRegio.Domain.Models.Components.Home;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Models.View.Article
{
    /// <summary>
    /// Klasa zawierająca elementy do wyświetlenia na stronie z listą artykułów
    /// </summary>
    public class ArticleBoxViewModel
    {
        /// <summary>
        /// Lista artykułów do wyświetlenia
        /// </summary>
        public IEnumerable<OfferBoxModel> ArticleList { get; set; }
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
    }
}
