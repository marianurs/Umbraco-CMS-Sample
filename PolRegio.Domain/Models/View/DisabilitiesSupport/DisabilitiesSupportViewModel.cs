using PolRegio.Domain.Models.Components;
using PolRegio.Domain.Models.UmbracoCreate;
using PolRegio.Domain.Models.View.AccordionPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Models.View.DisabilitiesSupport
{
    public class DisabilitiesSupportViewModel
    {
        /// <summary>
        /// Elementy artykułu pobrane z bazy
        /// </summary>
        public ArticleContent ArticleContent { get; set; }
        /// <summary>
        /// Lista elementów znajdujących się w karuzeli 
        /// </summary
        public IEnumerable<ArticleCarouselItem> CarouselImages { get; set; }
        /// <summary>
        /// Lista dokumentów do pobrania 
        /// </summary
        public IEnumerable<DownloadItem> DownloadDocuments { get; set; }

        public IEnumerable<AccordionViewModel> AccordionItems { get; set; }
    }
}
