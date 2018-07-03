using PolRegio.Cms.ContentEvents;
using PolRegio.Cms.ContentFinder;
using PolRegio.Domain.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Persistence;
using Umbraco.Core.Services;
using Umbraco.Web.Routing;

namespace PolRegio.Cms
{
    /// <summary>
    /// Klasa zawierająca obsługę customowych rozwiązań w CMS
    /// </summary>
    public class CustomCmsHandler : ApplicationEventHandler
    {
        protected const string AdditionalSettingT = "additionalsettings";
        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        public CustomCmsHandler() { }
        /// <summary>
        /// Nadpisanie metody ApplicationStarted
        /// Metoda zawiera customowe rozwiązania dla CMS
        /// </summary>
        /// <param name="umbracoApplication">obiekt UmbracoApplicationBase</param>
        /// <param name="applicationContext">obiekt ApplicationContext</param>
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            #region CustomTables
            var _db = applicationContext.DatabaseContext.Database;
            var _logger = LoggerResolver.Current.Logger;
            var _sqlSyntax = applicationContext.DatabaseContext.SqlSyntax;
            var _dbHelper = new DatabaseSchemaHelper(_db, _logger, _sqlSyntax);

            if (!_dbHelper.TableExist("PolRegioRegion"))
                _dbHelper.CreateTable<RegionDB>(true);

            if (!_dbHelper.TableExist("PolRegioArticleType"))
                _dbHelper.CreateTable<ArticleTypeDB>(true);

            if (!_dbHelper.TableExist("PolRegioAdministrative"))
                _dbHelper.CreateTable<AdministrativeDB>(true);
            #endregion

            var _contentServiceEvents = new ContentServiceEvents(applicationContext);
            ContentService.Saved += _contentServiceEvents.ContentService_Saved;
        }
        /// <summary>
        /// Nadpisanie metody ApplicationStarting
        /// Metoda zawiera customowe rozwiązania dla CMS
        /// </summary>
        /// <param name="umbracoApplication">UmbracoApplicationBase object</param>
        /// <param name="applicationContext">ApplicationContext object</param>
        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            ContentFinderResolver.Current.InsertTypeBefore<ContentFinderByNotFoundHandlers, Custom404ContentFinder>();
            base.ApplicationStarting(umbracoApplication, applicationContext);
        }
    }
}
