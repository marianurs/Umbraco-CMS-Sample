using PolRegio.Domain.Services.Accordions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PolRegio.Web.Controllers
{
    public class PolRegioAccordionController : PolRegioBaseController
    {
        /// <summary>
        /// prywatne pole IAccordionService
        /// </summary>
        private readonly IAccordionService _accordionService;
        /// <summary>
        /// Konstruktor kontrolera PolRegioAccordionController
        /// </summary>
        /// <param name="advertisingService">wstrzyknięty obiekt IAccordionService z DependencyInjection</param>
        public PolRegioAccordionController(IAccordionService accordionService)
        {
            _accordionService = accordionService;
        }
        /// <summary>
        /// Renderuje widok akordeonu
        /// </summary>
        /// <returns>Widok z akordeonem</returns>
        [ChildActionOnly]
        public ActionResult RenderAccordionPage()
        {
            var _model = _accordionService.GetAccordionElementsModel(CurrentPage.Id);
            return PartialView("AccordionPartial", _model);
        }

        /// <summary>
        /// Renderuje widok strony oddziałów
        /// </summary>
        /// <returns>Widok z oddziałów z akordeonem</returns>
        [ChildActionOnly]
        public ActionResult RenderOfficesAccordionPage()
        {
            var _model = _accordionService.GetAccordionElementsForOfficeModel(CurrentPage.Id);
            return PartialView("OfficesAccordionPartial", _model);
        }
    }
}