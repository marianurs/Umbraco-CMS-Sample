using PolRegio.Domain.Models.View.Article;
using PolRegio.Domain.Services.Article;
using PolRegio.Helpers.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace PolRegio.Web.Controllers
{
    public class PolRegioArticleController : PolRegioBaseController
    {
        /// <summary>
        /// prywatne pole IArticleService
        /// </summary>
        private readonly IArticleService _articleService;
        /// <summary>
        /// prywatne pole IBoxListService
        /// </summary>
        private readonly IBoxListService _boxService;
        /// <summary>
        /// Konstruktor kontrolera PolRegioNewsController
        /// </summary>
        /// <param name="layoutService">wstrzyknięty obiekt INewsService z DependencyInjection</param>
        public PolRegioArticleController(IArticleService articleService, IBoxListService boxService)
        {
            _articleService = articleService;
            _boxService = boxService;
        }
        [ChildActionOnly]
        public ActionResult RenderArticlePage()
        {
            var _model = _articleService.GetArticlePageViewModel(CurrentPage.Id);
            return PartialView("ArticlePartial", _model);
        }

        #region Oferty i promocje
        /// <summary>
        /// Renderuje pierwszy widok strony z listą ofert i promocji
        /// </summary>
        /// <returns>Widok z listą ofert i promocji</returns>
        [ChildActionOnly]
        public ActionResult RenderArticleListBoxesPage()
        {
            var _model = _boxService.GetArticleBoxesModel(CurrentPage.Id);
            return PartialView("BoxListPartial", _model);
        }
        /// <summary>
        /// Metoda zwraca widok artykułów 
        /// </summary>
        /// <param name="model">obiekt NewsPageViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [NotChildAction]
        public ActionResult RenderArticleListBoxes(ArticleBoxViewModel model)
        {
            SetCulture(model.CurrentPageCulture);

            var _model = _boxService.GetArticleBoxesModel(model);
            return PartialView("BoxListPartial", _model);
        }
        /// <summary>
        /// Metoda zwracająca listę obiektów po kliknięciu LoadMore
        /// </summary>
        /// <param name="skipCount">ilość elementów jaka jest na stronie</param>
        /// <param name="currentCulture">aktualny obiekt CultureInfo</param>
        /// <param name="displayCount">ilość elementów jaką trzeba pobrać</param>
        /// <param name="currentPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>List obiektów OfferBoxModel</returns>
        [HttpPost]
        [NotChildAction]
        public ActionResult LoadMoreArticleBoxes(int skipCount, string currentCulture, int displayCount, int currentPageId)
        {
            SetCulture(currentCulture);

            var _result = _boxService.GetMoreArticle(skipCount, displayCount, currentPageId);
            return PartialView("Controls/ArticleBoxesList", _result);
        }
        #endregion

        #region Oferty regionalne
        /// <summary>
        /// Renderuje pierwszy widok strony z listą ofert i promocji
        /// </summary>
        /// <returns>Widok z listą ofert i promocji</returns>
        [ChildActionOnly]
        public ActionResult RenderRegionalArticleListBoxesPage()
        {
            var _model = _boxService.GetRegionalArticleBoxesModel(CurrentPage.Id);
            return PartialView("RegionalBoxPartial", _model);
        }
        /// <summary>
        /// Metoda zwraca widok artykułów 
        /// </summary>
        /// <param name="model">obiekt NewsPageViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [NotChildAction]
        public ActionResult RenderRegionalArticleListBoxes(RegionalOfferBoxViewModel model)
        {
            SetCulture(model.CurrentPageCulture);
            //TempData[TempDataVariables.RegionOffersFiltr] = model.SelectedRegionId;
            var _model = _boxService.GetRegionalArticleBoxesModel(model);
            return PartialView("RegionalBoxPartial", _model);
        }
        /// <summary>
        /// Metoda zwracająca listę obiektów po kliknięciu LoadMore
        /// </summary>
        /// <param name="skipCount">ilość elementów jaka jest na stronie</param>
        /// <param name="currentCulture">aktualny obiekt CultureInfo</param>
        /// <param name="displayCount">ilość elementów jaką trzeba pobrać</param>
        /// <param name="currentPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>List obiektów OfferBoxModel</returns>
        [HttpPost]
        [NotChildAction]
        public ActionResult LoadMoreRegionalArticleBoxes(int selectedRegionId, int skipCount, string currentCulture, int displayCount, int currentPageId)
        {
            SetCulture(currentCulture);

            var _result = _boxService.GetMoreRegionalArticle(selectedRegionId, skipCount, displayCount, currentPageId);
            return PartialView("Controls/ArticleBoxesList", _result);
        }
        #endregion
    }
}