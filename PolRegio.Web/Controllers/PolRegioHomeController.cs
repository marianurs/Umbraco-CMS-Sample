using PolRegio.Domain.Services.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace PolRegio.Web.Controllers
{
    public class PolRegioHomeController : SurfaceController
    {
        /// <summary>
        /// prywatne pole IHomeService
        /// </summary>
        private readonly IHomeService _homeService;
        /// <summary>
        /// Konstruktor kontrolera PolRegioSurfaceController
        /// </summary>
        /// <param name="layoutService">wstrzyknięty obiekt IHomeService z DependencyInjection</param>
        public PolRegioHomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        [ChildActionOnly]
        public ActionResult RenderHomePage()
        {
            var _model = _homeService.GetHomePageViewModel(CurrentPage.Id);
            return PartialView("HomePartial", _model);
        }
    }
}