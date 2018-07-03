using PolRegio.Domain.Models.View.News;
using PolRegio.Domain.Services.News;
using PolRegio.Helpers.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace PolRegio.Web.Controllers
{
    public class PolRegioNewsController : PolRegioBaseController
    {
        /// <summary>
        /// prywatne pole INewsService
        /// </summary>
        private readonly INewsService _newsService;
        /// <summary>
        /// Konstruktor kontrolera PolRegioNewsController
        /// </summary>
        /// <param name="layoutService">wstrzyknięty obiekt INewsService z DependencyInjection</param>
        public PolRegioNewsController(INewsService newsService)
        {
            _newsService = newsService;
        }
        /// <summary>
        /// Renderuje pierwszy widok strony z artykułami
        /// </summary>
        /// <returns>Widok z listą artykułów</returns>
        [ChildActionOnly]
        public ActionResult RenderNewsPage()
        {
            var _typeFromUrl = UmbracoContext.HttpContext.Request.QueryString["type"];
            var _model = _newsService.GetNewsBoxesModel(CurrentPage.Id, _typeFromUrl);
            return PartialView("NewsPartial", _model);
        }
        /// <summary>
        /// Metoda zwraca widok artykułów 
        /// </summary>
        /// <param name="model">obiekt NewsPageViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [NotChildAction]
        public ActionResult RenderArticleBoxes(NewsPageViewModel model)
        {
            SetCulture(model.CurrentPageCulture);
            //TempData[TempDataVariables.NewsRegionFiltr] = model.SelectedRegionId;
            //TempData[TempDataVariables.NewsTypeFiltr] = model.NewsTypeFilter.Where(q => q.IsChecked).Select(q => q.Id).ToList();
            var _model = _newsService.GetNewsBoxesModel(model);
            return PartialView("NewsPartial", _model);
        }
        [HttpPost]
        [NotChildAction]
        public ActionResult LoadMoreArticleBoxes(int selectedRegionId, List<int> selectedTypeIds, int skipCount, string currentCulture, int displayCount, int currentPageId)
        {
            SetCulture(currentCulture);

            var _result = _newsService.GetMoreNews(selectedRegionId, selectedTypeIds, skipCount, displayCount, currentPageId);
            return PartialView("Controls/NewsBoxesList", _result);
        }
    }
}