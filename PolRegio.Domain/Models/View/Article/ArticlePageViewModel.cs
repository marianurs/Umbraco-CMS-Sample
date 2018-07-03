using PolRegio.Domain.Models.Components;
using PolRegio.Domain.Models.Components.Tag;
using PolRegio.Domain.Models.UmbracoCreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PolRegio.Domain.Models.View.Article
{
    /// <summary>
    /// Klasa zawierająca elementy wyświetlane na stronie artykułu
    /// </summary>
    public class ArticlePageViewModel
    {
        /// <summary>
        /// Elementy artykułu pobrane z bazy
        /// </summary>
        public ArticleContent ArticleContent { get; set; }
        public string ArticleType { get; set; }
        public string ArticleRegion { get; set; }
       
        ///// <summary>
        ///// Tytuł artykułu
        ///// </summary
        //public string ArticleTitle { get; set; }
        /// <summary>
        /// Lista elementów znajdujących się w karuzeli 
        /// </summary
        public IEnumerable<ArticleCarouselItem> CarouselImages { get; set; }
        ///// <summary>
        ///// Treść artykułu 
        ///// </summary
        //public IHtmlString ArticleBody { get; set; }
        /// <summary>
        /// Lista dokumentów do pobrania 
        /// </summary
        public IEnumerable<DownloadItem> DownloadDocuments { get; set; }

        ///// <summary>
        ///// Share twitter url
        ///// </summary>
        //public string ShareTwitterLink { get; set; }
        ///// <summary>
        ///// Share facebook url
        ///// </summary>
        //public string ShareFacebookLink { get; set; }
        /// <summary>
        /// Data dodania artykułu
        /// </summary>
        public DateTime? ArticleDate { get; set; }
        /// <summary>
        /// Adres url do strony nadrzędnej
        /// </summary>
        public string ParentUrl { get { return ArticleContent.Parent.Url; } }
        /// <summary>
        /// Lista tagów artykułu 
        /// </summary
        public string [] Tags { get; set; }
        public string TagListUrl { get; set; }
        /// <summary>
        /// Flaga oznaczająca czy artykuł jest z ofert i promocji
        /// </summary>
        public bool IsOffersArticle { get; set; }
    }
    public class RegionItem
    {
        public string key { get; set; }
        public string label { get; set; }
    }
}

