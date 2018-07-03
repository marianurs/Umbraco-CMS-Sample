using PolRegio.Domain.Services.DisabilitiesSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PolRegio.Web.Controllers
{
    public class PolRegioDisabilitiesSupportController : PolRegioBaseController
    {
        /// <summary>
        /// prywatne pole IDisabilitiesSupportService
        /// </summary>
        private readonly IDisabilitiesSupportService _disabilitiesSupport;

        /// <summary>
        /// Konstruktor kontrolera PolRegioDisabilitiesSupportController
        /// </summary>
        /// <param name="_disabilitiesSupport">wstrzyknięty obiekt IDisabilitiesSupportService z DependencyInjection</param>
        public PolRegioDisabilitiesSupportController(IDisabilitiesSupportService disabilitiesSupport)
        {
            _disabilitiesSupport = disabilitiesSupport;
        }
        [ChildActionOnly]
        public ActionResult RenderDisabilitiesSupport()
        {
            var _model = _disabilitiesSupport.GetDisabilitiesSupportVieww(CurrentPage.Id);
            return PartialView("DisabilitiesSupportPartial", _model);
        }
    }
}