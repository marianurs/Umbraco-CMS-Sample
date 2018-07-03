using PolRegio.Domain.Services.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using Umbraco.Core.Models;
using PolRegio.Helpers.Enums;
using Umbraco.Web;

namespace PolRegio.Services.Shared
{
    /// <summary>
    /// Klasa implementująca interfejs IUmbracoHelperService
    /// </summary>
    public class UmbracoHelperService : IUmbracoHelperService
    {
        /// <summary>
        /// Metoda zwracająca CUltureInfo dla danego noda
        /// </summary>
        /// <param name="content">Obiekt umbraco typu IPublishedContent</param>
        /// <returns></returns>
        public CultureInfo GetCulture(IPublishedContent content)
        {
            List<IPublishedContent> ancestors = content.AncestorsOrSelf(DocumentTypeEnum.location.ToString()).ToList();
            umbraco.cms.businesslogic.web.Domain[] domains = umbraco.cms.businesslogic.web.Domain.GetDomains(true).ToArray();
            foreach (IPublishedContent cnt in ancestors)
            {
                umbraco.cms.businesslogic.web.Domain nodeDomain = domains.Where(d => d.RootNodeId == cnt.Id).FirstOrDefault();
                if (nodeDomain != null)
                    return new CultureInfo(nodeDomain.Language.CultureAlias);
            }
            return new CultureInfo(domains.First().Language.CultureAlias);
        }
    }
}