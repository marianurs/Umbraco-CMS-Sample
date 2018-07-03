using PolRegio.Domain.Services.Bip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolRegio.Domain.Models.View.BipPage;
using Umbraco.Web;
using PolRegio.Domain.Models.UmbracoCreate;

namespace PolRegio.Services.Bip
{
    public class ArticleBipService : IBIPArticleService
    {
        /// <summary>
        /// Obiekt typu UmbracoHelper
        /// </summary>
        private UmbracoHelper _umbracoHelper;
        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        public ArticleBipService()
        {
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
        }
        public BIPArticleViewModel BIPArticleViewModel(int currentUmbracoPageId)
        {
            var _model = new BIPArticleViewModel();

            //Aktualna strona, na której się znajdujemy
            var _currentPage = _umbracoHelper.TypedContent(currentUmbracoPageId);
            _model.Article = new ArticleBip(_currentPage);
            return _model;

        }
    }
}
