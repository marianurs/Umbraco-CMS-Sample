using PolRegio.Domain.Models.Components.BIP;
using PolRegio.Domain.Models.UmbracoCreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Models.View.BipPage
{
    public class BIPArticleViewModel
    {
        /// <summary>
        /// Obiekt typu ArticleBip
        /// </summary>
        public ArticleBip Article { get; set; }
        /// <summary>
        /// Data utworzenia artykułu
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Osoba dodająca artykuł
        /// </summary>
        public string AuthorBip { get; set; }
        /// <summary>
        /// Adres url do strony nadrzędnej
        /// </summary>
        public string ParentUrl { get { return Article.Parent.Url; } }
        /// <summary>
        /// Lista zmian w artykule
        /// </summary>
        public List<ArticleBipChanges> ArticleChanges { get; set; }
    }
}
