using PolRegio.Domain.Models.View.BipPage;
using PolRegio.Domain.Services.BipModels;
using PolRegio.Domain.Services.Shared;
using reCAPTCHA.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace PolRegio.Web.Controllers
{
    public class PolRegioBIPController : PolRegioBaseController
    {
        /// <summary>
        /// prywatne pole IBIPService
        /// </summary>
        private readonly IBIPService _bipService;
        /// <summary>
        /// prywatne pole IEmailService
        /// </summary>
        private readonly IEmailService _emailService;
        /// <summary>
        /// Konstruktor kontrolera PolRegioNewsController
        /// </summary>
        /// <param name="bipService">wstrzyknięty obiekt IBIPService z DependencyInjection</param>
        public PolRegioBIPController(IBIPService bipService, IEmailService emailService)
        {
            _bipService = bipService;
            _emailService = emailService;
        }

        /// <summary>
        /// Renderuje widok strony z formularzem o udostępnienie
        /// informacji publicznej
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult RenderBipFormPage()
        {
            var _model = _bipService.GetBipFormView(CurrentPage.Id);
            return PartialView("BIPFormPartial", _model);
        }
        /// <summary>
        /// Metoda wysyła email do BIP
        /// </summary>
        /// <param name="model">obiekt BIPFormViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [NotChildAction]
        [ValidateAntiForgeryToken]
        [CaptchaValidator]
        public ActionResult SubmitBipFom(BIPFormViewModel model)
        {
            SetCulture(model.CurrentPageCulture);
            if (ModelState.IsValid)
            {
                var _result = _emailService.SendBIPEmail(model);
                model = _result;
            }
            return PartialView("BIPFormPartial", model);
        }
        /// <summary>
        /// Renderuje widok strony z formularzem o udostępnienie
        /// informacji publicznej
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult RenderArticleBip()
        {
            var _model = _bipService.BIPArticleViewModel(CurrentPage.Id);
            return PartialView("ArticleBipPartial", _model);
        }

        [ChildActionOnly]
        public ActionResult RenderMainPageBip()
        {
            var _model = _bipService.BIPPageViewModel(CurrentPage.Id);
            return PartialView("MainPageBipPartial", _model);
        }

        [HttpPost]
        [NotChildAction]
        public ActionResult SearchBipItems(BIPPageViewModel model)
        {
            SetCulture(model.CurrentPageCulture);
            var _model = _bipService.BIPPageViewModel(model);
            return PartialView("MainPageBipPartial", _model);
        }
    }
}