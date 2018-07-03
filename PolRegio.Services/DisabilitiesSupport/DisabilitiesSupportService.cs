using PolRegio.Domain.Services.DisabilitiesSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolRegio.Domain.Models.View.DisabilitiesSupport;
using Umbraco.Web;
using PolRegio.Domain.Models.UmbracoCreate;
using PolRegio.Domain.Models.Components;
using PolRegio.Domain.Models.View.AccordionPage;

namespace PolRegio.Services.DisabilitiesSupport
{
    /// <summary>
    /// Klasa implementująca interfejs IArticleService
    /// </summary>
    public class DisabilitiesSupportService : IDisabilitiesSupportService
    {
        /// <summary>
        /// Obiekt typu UmbracoHelper
        /// </summary>
        private UmbracoHelper _umbracoHelper;
        public DisabilitiesSupportService()
        {
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
        }

        public DisabilitiesSupportViewModel GetDisabilitiesSupportVieww(int currentUmbracoPageId)
        {
            var _currentArticle = _umbracoHelper.TypedContent(currentUmbracoPageId);

            var _model = new DisabilitiesSupportViewModel();
            _model.ArticleContent = new ArticleContent(_currentArticle);

            if (_model.ArticleContent.ArticleImageList != null)
            {
                _model.CarouselImages = _model.ArticleContent.ArticleImageList.Fieldsets.Where(x => x != null && x.Properties.Any() && !x.Disabled).Select(q => new ArticleCarouselItem()
                {
                    ArticleCarouselImage = q.GetValue<string>("addImage"),
                    ArticleCarouselDesc = q.GetValue<string>("imageCarouselDesc"),
                    ArticleCarouselName = q.GetValue<string>("imageName")
                });
            }
            if (_model.ArticleContent.AddDocumentDown != null)
            {
                _model.DownloadDocuments = _model.ArticleContent.AddDocumentDown.Fieldsets.Where(x => x != null && x.Properties.Any() && !x.Disabled).Select(q => new DownloadItem()
                {
                    DocumentUrl = q.GetValue<string>("addDoc"),
                    DocumentName = q.GetValue<string>("articleDocName"),
                    DocumentDate = q.GetValue<DateTime>("chooseDate")
                });
            }
            var _accordionWithAttachments = new AccordionWithAttachments(_currentArticle);

            if (_accordionWithAttachments.AccordionElements != null)
            {
                _model.AccordionItems = _accordionWithAttachments.AccordionElements.Fieldsets.Where(x => x != null && x.Properties.Any() && !x.Disabled).Select(q => new AccordionViewModel(q));
            }

            return _model;
        }
    }
}
