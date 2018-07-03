using PolRegio.Domain.Models.View.Tickets;
using PolRegio.Domain.Services.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace PolRegio.Web.Controllers
{
    public class PolRegioTicketOfficesController : PolRegioBaseController
    {
        /// <summary>
        /// prywatne pole ITicketOfficesService
        /// </summary>
        private readonly ITicketOfficesService _ticketService;
        /// <summary>
        /// Konstruktor kontrolera PolRegioTicketOfficesController
        /// </summary>
        /// <param name="ticketService">wstrzyknięty obiekt ITicketOfficesService z DependencyInjection</param>
        public PolRegioTicketOfficesController(ITicketOfficesService ticketService)
        {
            _ticketService = ticketService;
        }
        /// <summary>
        /// Renderuje pierwszy widok strony z artykułami
        /// </summary>
        /// <returns>Widok z listą artykułów</returns>
        [ChildActionOnly]
        public ActionResult RenderTicketOfficesPage()
        {
            var _model = _ticketService.GetTicketOfficesModel(CurrentPage.Id);
            return PartialView("TicketOfficesPartial", _model);
        }
        /// <summary>
        /// Metoda zwraca widok z listą zamówień 
        /// </summary>
        /// <param name="model">obiekt ContractNoticesViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [NotChildAction]
        public ActionResult RenderTicketOfficesBoxes(TicketOfficesPageViewModel model)
        {
            SetCulture(model.CurrentPageCulture);

            var _model = _ticketService.GetTicketOfficesModel(model);
            return PartialView("TicketOfficesPartial", _model);
        }
        /// <summary>
        /// Metoda zwracająca listę kolejnych elementó kas biletowych
        /// </summary>
        /// <param name="officeName">nazw kasy biletowej z filtra</param>
        /// <param name="skipCount">ilość elementów jaka jest na stronie</param>
        /// <param name="displayCount">ilość elementów jaką trzeba pobrać</param>
        /// <param name="currentPageId">id strony umbraco na której się znajdujemy</param>
        /// <param name="currentCulture">aktualny język an stronie</param>
        /// <returns>widok z listą elementów kas</returns>
        [HttpPost]
        [NotChildAction]
        public ActionResult LoadMoreTicketOfficesBoxes(string officeName, int skipCount, int displayCount, int currentPageId, string currentCulture)
        {
            SetCulture(currentCulture);

            var _result = _ticketService.GetMoreTicketOffices(officeName, skipCount, displayCount, currentPageId);
            return PartialView("Controls/TicketOfficesBoxList", _result);
        }
    }
}