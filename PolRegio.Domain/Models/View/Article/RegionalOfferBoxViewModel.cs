using PolRegio.Domain.Models.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Models.View.Article
{
    /// <summary>
    /// Klasa zawirająca pola do strony ofert regionalnych
    /// </summary>
    public class RegionalOfferBoxViewModel : ArticleBoxViewModel
    {
        /// <summary>
        /// Enumerator zawierający filtr regionu
        /// </summary>
        public List<FilterItem> RegionFilter { get; set; }
        /// <summary>
        /// Wybrany filtr z dropdown
        /// </summary>
        public int SelectedRegionId { get; set; }
    }
}
