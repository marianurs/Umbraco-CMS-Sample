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
    /// Kontroller zawierający obsługę obiektów typy RegionDB w CMS Umbraco
    /// Używany w customowej sekcji w CMS
    /// </summary>
    [PluginController("AdditionalSettings")]
    public class RegionDBApiController : UmbracoAuthorizedJsonController
    {
        /// <summary>
        /// Metoda zwraca wszytskie elementy z tabeli polregioregion
        /// </summary>
        /// <returns>Enumerable of RegionDB</returns>
        public IEnumerable<RegionDB> GetAll()
        {
            var query = new Sql().Select("*").From("polregioregion");
            return DatabaseContext.Database.Fetch<RegionDB>(query);
        }
        /// <summary>
        /// Metoda zwracająca obiekt regiony po id
        /// </summary>
        /// <param name="id">RegionDB id</param>
        /// <returns>RegionDB</returns>
        public RegionDB GetById(int id)
        {
            var query = new Sql().Select("*").From("polregioregion").Where<RegionDB>(x => x.Id == id);
            return DatabaseContext.Database.Fetch<RegionDB>(query).FirstOrDefault();
        }
        /// <summary>
        /// Metoda dodająca lub edytująca obiekt regionu
        /// </summary>
        /// <param name="serviceitem">RegionDB item</param>
        /// <returns>RegionDB</returns>
        public RegionDB PostSave(RegionDB serviceitem)
        {
            serviceitem.DictionaryKey = serviceitem.DictionaryKey.Replace(" ", "_");

            if (serviceitem.Id > 0)
                DatabaseContext.Database.Update(serviceitem);
            else
                DatabaseContext.Database.Save(serviceitem);

            if (!Services.LocalizationService.DictionaryItemExists(serviceitem.DictionaryKey))
            {
                var dictionaryItem = Services.LocalizationService.CreateDictionaryItemWithIdentity(serviceitem.DictionaryKey,Guid.Parse("A7B9CD7F-FE1A-436F-8408-77AA589DA5A1"));
                Services.LocalizationService.Save(dictionaryItem);
            }

            return serviceitem;
        }
    }
}
