using PolRegio.Domain.Models.View.Search;
using PolRegio.Domain.Services.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace PolRegio.Web.Controllers
{
    public class PolRegioSearchController : PolRegioBaseController
    {
        private readonly ISearchService _searchService;

        public PolRegioSearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }
        [ChildActionOnly]
        public ActionResult RenderSearchPage()
        {
            var _model = _searchService.GetSearchViewModel(CurrentPage.Id);
            return PartialView("SearchPartial", _model);
        }

        [HttpPost]
        [NotChildAction]
        public ActionResult RenderArticleBoxes(SearchViewModel searchModel)
        {
            SetCulture(searchModel.CurrentPageCulture);

            var _model = _searchService.SearchResult(searchModel);
            return PartialView("SearchPartial", _model);
        }
        [HttpPost]
        [NotChildAction]
        public ActionResult LoadMoreSearchResult(string name, int skipCount, int displayCount, int currentPageId, string currentCulture)
        {
            SetCulture(currentCulture);

            var _result = _searchService.GetMoreSearchResult(name, skipCount, displayCount, currentPageId);
            return PartialView("Controls/SearchList", _result);
        }
    }
}