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
    /// Kontroller zawierający obsługę obiektów typy AdministrativeDB w CMS Umbraco
    /// Używany w customowej sekcji w CMS
    /// </summary>
    [PluginController("AdditionalSettings")]
    public class AdministrativeDBApiController : UmbracoAuthorizedJsonController
    {
        /// <summary>
        /// Metoda zwraca wszytskie elementy z tabeli polregioadministrative
        /// </summary>
        /// <returns>Enumerable of AdministrativeDB</returns>
        public IEnumerable<AdministrativeDB> GetAll()
        {
            var query = new Sql().Select("*").From("polregioadministrative");
            return DatabaseContext.Database.Fetch<AdministrativeDB>(query);
        }
        /// <summary>
        /// Metoda zwracająca obiekt regiony po id
        /// </summary>
        /// <param name="id">AdministrativeDB id</param>
        /// <returns>AdministrativeDB</returns>
        public AdministrativeDB GetById(int id)
        {
            var query = new Sql().Select("*").From("polregioadministrative").Where<AdministrativeDB>(x => x.Id == id);
            return DatabaseContext.Database.Fetch<AdministrativeDB>(query).FirstOrDefault();
        }
        /// <summary>
        /// Metoda dodająca lub edytująca obiekt regionu
        /// </summary>
        /// <param name="serviceitem">AdministrativeDB item</param>
        /// <returns>AdministrativeDB</returns>
        public AdministrativeDB PostSave(AdministrativeDB serviceitem)
        {
            serviceitem.DictionaryKey = serviceitem.DictionaryKey.Replace(" ", "_");

            if (serviceitem.Id > 0)
                DatabaseContext.Database.Update(serviceitem);
            else
                DatabaseContext.Database.Save(serviceitem);

            if (!Services.LocalizationService.DictionaryItemExists(serviceitem.DictionaryKey))
            {
                var dictionaryItem = Services.LocalizationService.CreateDictionaryItemWithIdentity(serviceitem.DictionaryKey,Guid.Parse("F12E8859-8356-4E6F-826A-A7773C6A567F"));
                Services.LocalizationService.Save(dictionaryItem);
            }

            return serviceitem;
        }
    }
}
