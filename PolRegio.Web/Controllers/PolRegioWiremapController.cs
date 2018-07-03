using PolRegio.Domain.Services.Wiremaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PolRegio.Web.Controllers
{
    public class PolRegioWiremapController : PolRegioBaseController
    {
        private readonly IWiremapService _wiremapService;

        public PolRegioWiremapController(IWiremapService wiremapService)
        {
            _wiremapService = wiremapService;
        }
        [ChildActionOnly]
        public ActionResult RenderWiremapPage()
        {
            var _model = _wiremapService.GetWiremapPageViewModel(CurrentPage.Id);
            return PartialView("WiremapPartial", _model);
        }
    }
}