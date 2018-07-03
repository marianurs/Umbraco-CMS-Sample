using PolRegio.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Routing;

namespace PolRegio.Cms.ContentFinder
{
    /// <summary>
    /// Klasa zawierająca customowe rozwiązanie dla strony 404
    /// </summary>
    public class Custom404ContentFinder : IContentFinder
    {
        /// <summary>
        /// Metoda służy do znalezienia odpowiedniej strony 404 dla danego języka
        /// </summary>
        /// <param name="contentRequest"></param>
        /// <returns></returns>
        public bool TryFindContent(PublishedContentRequest contentRequest)
        {
            var _queryStringSegments = contentRequest.Uri.PathAndQuery.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var _errorPage = Get404PageForRegion(_queryStringSegments);
            if (_errorPage != null)
            {
                contentRequest.PublishedContent = _errorPage;
            }
            else
            {
                contentRequest.PublishedContent = new UmbracoHelper(UmbracoContext.Current).TypedContentAtRoot().First().Descendant(DocumentTypeEnum.error404.ToString());
            }
            return contentRequest.PublishedContent != null;
        }
        /// <summary>
        /// Metoda służy do znalezienia strony 404 dla danej lokalizacji
        /// </summary>
        /// <param name="queryStringSegments"></param>
        /// <returns></returns>
        private IPublishedContent Get404PageForRegion(IEnumerable<string> queryStringSegments)
        {
            var _closesUmbracoNode = GetClosestMatch(queryStringSegments);
            if (_closesUmbracoNode != null)
            {
                IPublishedContent _languageNode;
                if (_closesUmbracoNode.DocumentTypeAlias == DocumentTypeEnum.domain.ToString())
                {
                    _languageNode = _closesUmbracoNode.Children.Where("Visible").FirstOrDefault();
                }
                else
                {
                    _languageNode = _closesUmbracoNode.AncestorOrSelf(DocumentTypeEnum.location.ToString());
                }

                if (_languageNode != null)
                {
                    return _languageNode.Descendant(DocumentTypeEnum.error404.ToString());
                }
            }
            return null;
        }
        /// <summary>
        /// Metoda służy do znalezienia najbliższego istniejącego kontentu po url
        /// </summary>
        /// <param name="queryStringSegments"></param>
        /// <returns></returns>
        private IPublishedContent GetClosestMatch(IEnumerable<string> queryStringSegments)
        {
            var _findedLocalization = new UmbracoHelper(UmbracoContext.Current).TypedContentAtXPath("root//*[@urlName='" + queryStringSegments.First() + "']").FirstOrDefault();
            if (_findedLocalization != null)
            {
                foreach (var segment in queryStringSegments.Skip(1))
                {
                    var _found = _findedLocalization.Children().FirstOrDefault(x => MatchesUrl(segment, x));
                    if (_found == null)
                    {
                        break;
                    }
                    _findedLocalization = _found;
                }
            }
            return _findedLocalization;
        }
        /// <summary>
        /// Metoda sprawdzająca czy dany segment należy do danego noda z CMS
        /// </summary>
        /// <param name="urlSegment"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        private static bool MatchesUrl(string urlSegment, IPublishedContent content)
        {
            var _urlName = content.GetPropertyValue<string>("umbracoUrlName");
            var _urlDefault = umbraco.cms.helpers.url.FormatUrl(content.Name).ToLower();

            return _urlName == urlSegment || _urlDefault == urlSegment;
        }
    }
}
