using PolRegio.Domain.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Persistence;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;

namespace PolRegio.Cms.Controllers
{
    /// <summary>
    /// Kontroller zawierający obsługę obiektów typy ArticleTypeDB w CMS Umbraco
    /// Używany w customowej sekcji w CMS
    /// </summary>
    [PluginController("AdditionalSettings")]
    public class ArticleTypeDBApiController : UmbracoAuthorizedJsonController
    {
        /// <summary>
        /// Metoda zwraca wszytskie elementy z tabeli polregioarticletype
        /// </summary>
        /// <returns>Enumerable of ArticleTypeDB</returns>
        public IEnumerable<ArticleTypeDB> GetAll()
        {
            var query = new Sql().Select("*").From("polregioarticletype");
            return DatabaseContext.Database.Fetch<ArticleTypeDB>(query);
        }
        /// <summary>
        /// Metoda zwracająca obiekt regiony po id
        /// </summary>
        /// <param name="id">ArticleTypeDB id</param>
        /// <returns>ArticleTypeDB</returns>
        public ArticleTypeDB GetById(int id)
        {
            var query = new Sql().Select("*").From("polregioarticletype").Where<ArticleTypeDB>(x => x.Id == id);
            return DatabaseContext.Database.Fetch<ArticleTypeDB>(query).FirstOrDefault();
        }
        /// <summary>
        /// Metoda dodająca lub edytująca obiekt kategorii artykułu
        /// </summary>
        /// <param name="serviceitem">ArticleTypeDB item</param>
        /// <returns>ArticleTypeDB</returns>
        public ArticleTypeDB PostSave(ArticleTypeDB serviceitem)
        {
            serviceitem.DictionaryKey = serviceitem.DictionaryKey.Replace(" ", "_");

            if (serviceitem.Id > 0)
                DatabaseContext.Database.Update(serviceitem);
            else
                DatabaseContext.Database.Save(serviceitem);

            if (!Services.LocalizationService.DictionaryItemExists(serviceitem.DictionaryKey))
            {
                var dictionaryItem = Services.LocalizationService.CreateDictionaryItemWithIdentity(serviceitem.DictionaryKey, Guid.Parse("53B7C627-7EF8-4A0B-8AA0-7EEF7A8FF6C6"));
                Services.LocalizationService.Save(dictionaryItem);
            }

            return serviceitem;
        }
    }
}
