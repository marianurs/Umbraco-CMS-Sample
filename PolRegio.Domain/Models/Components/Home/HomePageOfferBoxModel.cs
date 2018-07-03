using PolRegio.Domain.Models.UmbracoCreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace PolRegio.Domain.Models.Components.Home
{
    /// <summary>
    /// Klasa zawierająca obiekt boxa z ofert i promocji
    /// </summary>
    public class OfferBoxModel : ArticleBox
    {
        /// <summary>
        /// Konstuktor klasy 
        /// </summary>
        /// <param name="content">Obiekt IPublishedContent (obiekt oferty z CMS)</param>
        public OfferBoxModel(IPublishedContent content) : base(content)
        {
        }
        /// <summary>
        /// Url na który ma kierować box
        /// </summary>
        public string BoxUrl { get { return Content.Url; } }
    }
}
