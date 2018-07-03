using PolRegio.Domain.Models.View.AdvertisingOfSalesModel;
using PolRegio.Domain.Services.AdvertisingOfSalesModel;
using PolRegio.Helpers.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace PolRegio.Web.Controllers
{
    public class PolRegioAdvertisingOfSalesController : PolRegioBaseController
    {
        /// <summary>
        /// prywatne pole IAdvertisingOfSalesService
        /// </summary>
        private readonly IAdvertisingOfSalesService _advertisingService;
        /// <summary>
        /// Konstruktor kontrolera PolRegioAdvertisingOfSalesController
        /// </summary>
        /// <param name="advertisingService">wstrzyknięty obiekt IAdvertisingOfSalesService z DependencyInjection</param>
        public PolRegioAdvertisingOfSalesController(IAdvertisingOfSalesService advertisingService)
        {
            _advertisingService = advertisingService;
        }
        /// <summary>
        /// Renderuje pierwszy widok strony z postępowaniami
        /// </summary>
        /// <returns>Widok z listą postępowań</returns>
        [ChildActionOnly]
        public ActionResult RenderAdvertisingOfSalesPage()
        {
            var _model = _advertisingService.GetProcedureBoxesModel(CurrentPage.Id, TempData);
            return PartialView("AdvertisingOfSalesPartial", _model);
        }
        /// <summary>
        /// Metoda zwraca widok z listą postępowań 
        /// </summary>
        /// <param name="model">obiekt AdvertisingOfSalesViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [NotChildAction]
        public ActionResult RenderAdvertisingOfSalesBoxes(AdvertisingOfSalesViewModel model)
        {
            SetCulture(model.CurrentPageCulture);
            //TempData[TempDataVariables.AdvertisingOfSalesStatus] = model.SelectedStatusId;
            //TempData[TempDataVariables.AdvertisingOfSalesAdministrative] = model.SelectedAdministrativeId;
            //TempData[TempDataVariables.AdvertisingOfSalesName] = model.Name;
            //TempData[TempDataVariables.AdvertisingOfSalesNumber] = model.Number;
            //TempData[TempDataVariables.AdvertisingOfSalesStartDate] = model.StartDate;
            //TempData[TempDataVariables.AdvertisingOfSalesEndDate] = model.EndDate;

            var _model = _advertisingService.GetProcedureBoxesModel(model);
            return PartialView("AdvertisingOfSalesPartial", _model);
        }

        [HttpPost]
        [NotChildAction]
        public ActionResult LoadMoreAdvertisingOfSalesBoxes(int selectedAdministrativeId, int selectedStatusId, DateTime? startDate, DateTime? endDate, string name, string number, int skipCount, int displayCount, int currentPageId, string currentCulture)
        {
            SetCulture(currentCulture);

            var _result = _advertisingService.GetMoreProcedure(selectedAdministrativeId, selectedStatusId, startDate, endDate, name, number, skipCount, displayCount, currentPageId);
            return PartialView("Controls/ProcedureBoxList", _result);
        }

        /// <summary>
        /// Renderuje szczegóły postępowania o sprzedaży
        /// </summary>
        /// <returns>Widok szczegółów postępowania o sprzedaży</returns>
        [ChildActionOnly]
        public ActionResult RenderAdvertisingOfSalesDetailsPage()
        {
            var _model = _advertisingService.GetProcedureDetails(CurrentPage.Id);
            return PartialView("AnnouncementPartial", _model);
        }
    }
}