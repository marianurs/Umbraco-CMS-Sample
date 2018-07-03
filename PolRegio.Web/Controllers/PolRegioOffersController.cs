using PolRegio.Domain.Models.View.News;
using PolRegio.Domain.Services.Offers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace PolRegio.Web.Controllers
{
    public class PolRegioOffersController : PolRegioBaseController
    {
        private readonly IOffersService _offersService;

        public PolRegioOffersController(IOffersService offersService)
        {
            _offersService = offersService;
        }
       [ChildActionOnly]
        public ActionResult RenderOffersPage()
        {
            var _model = _offersService.GetOffersPageViewModel(CurrentPage.Id);
            return PartialView("OffersPartial", _model);
        }
    }
}