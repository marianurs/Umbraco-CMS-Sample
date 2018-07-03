using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Models.Components
{
    /// <summary>
    /// Klasa zawierająca zdjęcie w karuzeli na stronie artykułu
    /// </summary>
    public class ArticleCarouselItem
    {
        /// <summary>
        /// Nazwa zdjęcia w karuzeli
        /// </summary>
        public string ArticleCarouselName { get; set; }
        /// <summary>
        /// Opis zdjęcia w karuzeli
        /// </summary>
        public string ArticleCarouselDesc { get; set; }
        /// <summary>
        /// Url do zdjęcia w karuzeli
        /// </summary>
        public string ArticleCarouselImage { get; set; }
    }
}
