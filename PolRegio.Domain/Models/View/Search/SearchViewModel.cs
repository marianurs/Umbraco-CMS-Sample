using PolRegio.Domain.Models.Components.Search;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Models.View.Search
{
    public class SearchViewModel
    {
        /// <summary>
        /// Tytuł strony
        /// </summary>
        public string SearchTitle { get; set; }
        /// <summary>
        /// Lista obiektów do wyświetlenia
        /// </summary>
        public List<SearchItem> SearchItems { get; set; }
        public string SearchText { get; set; }
        /// <summary>
        /// Liczba wszystkich elementów do wyświetlenia
        /// </summary>
        public int AllNewsCount { get; set; }
        /// <summary>
        /// Liczba wyświetlanych elementów po LoadMore
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
