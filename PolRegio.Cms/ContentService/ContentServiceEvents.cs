using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace PolRegio.Cms.ContentEvents
{
    /// <summary>
    /// Klasa zawierająca customowe rozwiązania dotyczące contentu z CMS
    /// </summary>
    public class ContentServiceEvents
    {
        /// <summary>
        /// Obiekt ApplicationContext
        /// </summary>
        private ApplicationContext _applicationContext;
        /// <summary>
        /// Konstruktor klasy ContentServiceEvents
        /// </summary>
        /// <param name="applicationContext"></param>
        public ContentServiceEvents(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        /// <summary>
        /// Metoda uruchamiana po zapisaniu noda w CMS.
        /// Metoda służy do zamiany polskich znaków w adresach url
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ContentService_Saved(IContentService sender, SaveEventArgs<IContent> e)
        {
            foreach (IContent item in e.SavedEntities)
            {
                bool _updateItem = false; 

                if (item.HasProperty("umbracoUrlName"))
                {
                    var _urlName = item.GetValue<string>("umbracoUrlName");
                    if (string.IsNullOrEmpty(_urlName))
                    {
                        var _itemName = item.Name;
                        var _resultUrl = Encoding.ASCII.GetString(Encoding.GetEncoding("Cyrillic").GetBytes(_itemName));
                        _resultUrl = Regex.Replace(_resultUrl, @"^\W+|\W+$", "");
                        _resultUrl = Regex.Replace(_resultUrl, @"_", " - ");
                        _resultUrl = Regex.Replace(_resultUrl, @"\W+", "-");

                        item.SetValue("umbracoUrlName", _resultUrl.ToLower());
                        _updateItem = true;
                    }
                }

                //Przypisanie z automatu name do pageTitle w zakładce SEO
                if (item.HasProperty("pageTitle") && string.IsNullOrEmpty(item.GetValue<string>("pageTitle")))
                {
                    item.SetValue("pageTitle", item.Name);
                    _updateItem = true;
                }
                //Przypisanie z automatu name do ogTitle w zakładce SEO
                if (item.HasProperty("ogTitle") && string.IsNullOrEmpty(item.GetValue<string>("ogTitle")))
                {
                    item.SetValue("ogTitle", item.Name);
                    _updateItem = true;
                }

                if (_updateItem)
                {
                    if (item.Published)
                    {
                        _applicationContext.Services.ContentService.SaveAndPublishWithStatus(item);
                    }
                    else
                    {
                        _applicationContext.Services.ContentService.Save(item);
                    }
                }
            }
        }
    }
}
