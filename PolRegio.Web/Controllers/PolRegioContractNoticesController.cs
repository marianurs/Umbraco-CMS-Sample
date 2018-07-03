using PolRegio.Domain.Models.View.ContractNoticeModel;
using PolRegio.Domain.Services.Notice;
using PolRegio.Helpers.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace PolRegio.Web.Controllers
{
    public class PolRegioContractNoticesController : PolRegioBaseController
    {
        /// <summary>
        /// prywatne pole IContractNoticesService
        /// </summary>
        private readonly IContractNoticesService _noticesService;
        /// <summary>
        /// Konstruktor kontrolera PolRegioContractNoticesController
        /// </summary>
        /// <param name="advertisingService">wstrzyknięty obiekt IContractNoticesService z DependencyInjection</param>
        public PolRegioContractNoticesController(IContractNoticesService noticesService)
        {
            _noticesService = noticesService;
        }

        /// <summary>
        /// Renderuje pierwszy widok strony z zamówieniami
        /// </summary>
        /// <returns>Widok z listą zamóień</returns>
        [ChildActionOnly]
        public ActionResult RenderNoticesPage()
        {
            var _model = _noticesService.GetNoticeBoxesModel(CurrentPage.Id, TempData);
            return PartialView("NoticesPartial", _model);
        }
        /// <summary>
        /// Metoda zwraca widok z listą zamówień 
        /// </summary>
        /// <param name="model">obiekt ContractNoticesViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [NotChildAction]
        public ActionResult RenderNoticesBoxes(ContractNoticesViewModel model)
        {
            SetCulture(model.CurrentPageCulture);

            var _model = _noticesService.GetNoticeBoxesModel(model);
            return PartialView("NoticesPartial", _model);
        }

        [HttpPost]
        [NotChildAction]
        public ActionResult LoadMoreNoticeBoxes(int selectedAdministrativeId, int selectedStatusId, int selectedLawActId, int selectedTypeOfContractId, DateTime? startDate, DateTime? endDate, string name, string number, int skipCount, int displayCount, int currentPageId, string currentCulture)
        {
            SetCulture(currentCulture);

            var _result = _noticesService.GetMoreNotice(selectedAdministrativeId, selectedStatusId,selectedLawActId,selectedTypeOfContractId, startDate, endDate, name, number, skipCount, displayCount, currentPageId);
            return PartialView("Controls/NoticesBoxList", _result);
        }

        /// <summary>
        /// Renderuje widok szczegółów zamówienia publicznego
        /// </summary>
        /// <returns>Widok ze szczegółami zamówienia</returns>
        [ChildActionOnly]
        public ActionResult RenderNoticesDetailPage()
        {
            var _model = _noticesService.GetNoticeDetails(CurrentPage.Id);
            return PartialView("NoticePartial", _model);
        }
    }
}