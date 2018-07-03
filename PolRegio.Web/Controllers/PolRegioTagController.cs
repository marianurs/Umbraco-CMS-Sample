using PolRegio.Domain.Services.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace PolRegio.Web.Controllers
{
    public class PolRegioTagController : SurfaceController
    {
        private readonly ITagsService _tagsService;

        public PolRegioTagController(ITagsService tagsService)
        {
            _tagsService = tagsService;
        }

        [ChildActionOnly]
        public ActionResult RenderTagsPage()
        {
            string tagText = Request.QueryString["tag"];
            if (!String.IsNullOrEmpty(tagText))
            {
                var _model = _tagsService.SearchResult(CurrentPage.Id, tagText);
                return PartialView("TagsPartial", _model);
            }
            else
            {
                var _model = _tagsService.errorPage(CurrentPage.Id);
                return View("~/Views/Error404.cshtml", _model);
            }
        }
    }
}