using PolRegio.Domain.Models.ISS;
using PolRegio.Domain.Models.View.SearchTicket;
using PolRegio.Domain.Services.ISS;
using PolRegio.Helpers.Enums;
using System.Web.Mvc;
using PolRegio.Domain.Services.EPodroznik;
using PolRegio.Domain.Services.Koleo;
using Umbraco.Web.Mvc;

namespace PolRegio.Web.Controllers
{
    /// <summary>
    /// Kontroler odpowiedzialny za zwrócenie 
    /// </summary>
    public class PolRegioISSController : PolRegioBaseController
    {
        private readonly IISSApiService _iissService;
        private readonly IEPodroznikService _ePodroznikService;
        private readonly IKoleoService _koleoService;

        public PolRegioISSController(IISSApiService iissService, IEPodroznikService ePodroznikService, IKoleoService koleoService)
        {
            _iissService = iissService;
            _ePodroznikService = ePodroznikService;
            _koleoService = koleoService;
        }

        /// <summary>
        /// Metoda zwracająca listę stacji kolejowych
        /// </summary>
        /// <returns></returns>
        [NotChildAction]
        public ActionResult GetStations()
        {
            return Content(_iissService.GetAdditionalInformationForType());
        }


        [NotChildAction]
        [HttpPost]
        public ActionResult SearchTicket(SearchTicketFormView model)
        {
            SetCulture(model.CurrentPageCulture);
            if (ModelState.IsValid)
            {
                _iissService.GetAnswerKeyToRedirectToBuyTicket(ref model);
                _ePodroznikService.SetRedirectUrl(ref model);
                _koleoService.SetRedirectUrl(ref model);
            }
            else
            {
                model.AnswerKeyResponse = new ISSResponseModel() { IsError = true, ErrorCode = "666" };
            }
            return PartialView("Controls/SearchTicket", model);
        }
    }
}