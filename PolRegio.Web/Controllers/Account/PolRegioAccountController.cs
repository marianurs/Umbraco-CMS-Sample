using PolRegio.Domain.Models.View.Account;
using PolRegio.Domain.Services.Account;
using PolRegio.Helpers.Constants;
using PolRegio.Helpers.Extensions;
using PolRegio.Web.Filters;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace PolRegio.Web.Controllers
{
    public class PolRegioAccountController : PolRegioBaseController
    {
        private readonly UmbracoHelper _umbracoHelper;
        private readonly IAccountService _accountService;
        private readonly ISocialMediaService _socialMediaService;

        public PolRegioAccountController(IAccountService accountService, ISocialMediaService socialMediaService)
        {
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
            _accountService = accountService;
            _socialMediaService = socialMediaService;
        }

        [ChildActionOnly]
        public ActionResult RenderProfilePage()
        {
            var model = _accountService.GetProfileView(CurrentPage.Id, new ProfileViewModel());
            return PartialView("ProfilePartial", model);
        }

        [HttpPost]
        [NotChildAction]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitProfileForm(ProfileViewModel model)
        {
            SetCulture(model.CurrentPageCulture);

            if (ModelState.IsValid)
            {
                model.Response = _accountService.SubmitProfile(model);
                if (model.Response.IsError && model.Response.ValidationErrors.Any())
                {
                    MapErrorsToModelState(model.Response.ValidationErrors);
                }
            }

            model = _accountService.GetProfileView(model.CurrentUmbracoPageId, model);
            return PartialView("ProfilePartial", model);
        }

        [ChildActionOnly]
        public ActionResult RenderRegisterFormPage()
        {
            var model = _accountService.GetRegisterFormView(CurrentPage.Id, new RegisterFormViewModel());
            return PartialView("RegisterFormPartial", model);
        }

        [HttpPost]
        [NotChildAction]
        [ValidateAntiForgeryToken]
        [GuestsOnly]
        public ActionResult SubmitRegisterForm(RegisterFormViewModel model)
        {
            SetCulture(model.CurrentPageCulture);

            if (ModelState.IsValid)
            {
                model.Response = _accountService.Register(model);
                if (model.Response.IsError && model.Response.ValidationErrors.Any())
                {
                    MapErrorsToModelState(model.Response.ValidationErrors);
                }
            }

            model = _accountService.GetRegisterFormView(model.CurrentUmbracoPageId, model);
            return PartialView("RegisterFormPartial", model);
        }

        [ChildActionOnly]
        public ActionResult RenderLoginFormPage()
        {
            var model = _accountService.GetLoginFormView(CurrentPage.Id, new LoginFormViewModel());

            return PartialView("LoginFormPartial", model);
        }

        [HttpPost]
        [NotChildAction]
        [ValidateAntiForgeryToken]
        [GuestsOnly]
        public ActionResult SubmitLoginForm(LoginFormViewModel model)
        {
            SetCulture(model.CurrentPageCulture);
            if (ModelState.IsValid)
            {
                model.Response = _accountService.Login(model);
                if (!model.Response.IsError) CookiesExtensions.CreateCookie(CookieVariables.TempMessageCookie, model.Response.Message);
            }

            model = _accountService.GetLoginFormView(model.CurrentUmbracoPageId, model);
            return PartialView("LoginFormPartial", model);
        }

        public ActionResult SocialMediaLogin(string lang, SocialMediaFormViewModel model)
        {
            if (!_socialMediaService.ValidateToken(model))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var isSuccess = _accountService.TryLoginUsingSocialMedia(model);

            if (isSuccess) CookiesExtensions.CreateCookie(CookieVariables.TempMessageCookie, "Login.Submit.Success");

            return Json(new { LoggedIn = isSuccess });
        }

        [ChildActionOnly]
        public ActionResult RenderSocialMediaForm()
        {
            var model = _accountService.GetSocialMediaFormView(CurrentPage.Id, new SocialMediaFormViewModel());
            return PartialView("SocialMediaFormPartial", model);
        }

        [HttpPost]
        [NotChildAction]
        [ValidateAntiForgeryToken]
        [GuestsOnly]
        public ActionResult SubmitSocialMediaForm(SocialMediaFormViewModel model)
        {
            if (!_socialMediaService.ValidateToken(model))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            SetCulture(model.CurrentPageCulture);

            if (ModelState.IsValid)
            {
                model.Response = _accountService.RegisterUsingSocialMedia(model);

                if (model.Response.IsError && model.Response.ValidationErrors.Any())
                {
                    MapErrorsToModelState(model.Response.ValidationErrors);
                }

                CookiesExtensions.CreateCookie(CookieVariables.TempMessageCookie, model.Response.Message);
            }

            model = _accountService.GetSocialMediaFormView(model.CurrentUmbracoPageId, model);
            return PartialView("SocialMediaFormPartial", model);
        }

        [AuthorizedOnly]
        public ActionResult Logout(string lang)
        {
            _accountService.Logout();

            TempData["Message"] = "Logout.Submit.Success";

            return Redirect(AccountRedirectUrls.Home(lang));
        }

        [HttpPost]
        [NotChildAction]
        [AuthorizedOnly]
        public ActionResult DeleteAccount()
        {
            var result = _accountService.DeleteAccount();

            if (result)
                TempData["Message"] = "DeleteAccount.Submit.Success";

            return Json(new { IsSuccess = result });
        }

        [ChildActionOnly]
        public ActionResult RenderForgotPassForm()
        {
            var model = _accountService.GetForgotPassFormView(base.CurrentPage.Id, new ForgotPassFormViewModel());
            return PartialView("ForgotFormPartial", model);
        }

        [HttpPost]
        [NotChildAction]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForgotPassForm(ForgotPassFormViewModel model)
        {
            SetCulture(model.CurrentPageCulture);
            if (ModelState.IsValid)
            {
                model.Response = _accountService.ForgotPass(model);
                if (!model.Response.IsError)
                {
                    CookiesExtensions.CreateCookie(CookieVariables.TempMessageCookie, model.Response.Message);
                }
            }

            model = _accountService.GetForgotPassFormView(model.CurrentUmbracoPageId, model);
            return PartialView("ForgotFormPartial", model);
        }

        [ChildActionOnly]
        public ActionResult RenderResetPassForm()
        {
            var model = new ResetPassFormViewModel
            {
                Token = Request.QueryString["token"]
            };
            model = _accountService.GetResetPassFormView(base.CurrentPage.Id, model);

            return PartialView("ResetFormPartial", model);
        }

        [HttpPost]
        [NotChildAction]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitResetPassForm(ResetPassFormViewModel model)
        {
            SetCulture(model.CurrentPageCulture);
            if (ModelState.IsValid)
            {
                model.Response = _accountService.ResetPass(model);
                if (!model.Response.IsError) CookiesExtensions.CreateCookie(CookieVariables.TempMessageCookie, model.Response.Message);
            }

            model = _accountService.GetResetPassFormView(model.CurrentUmbracoPageId, model);
            return PartialView("ResetFormPartial", model);
        }

        public ActionResult ActivateAccount(string lang, string token)
        {
            var loginPageUrl = AccountRedirectUrls.LoginPage(lang);
            var result = _accountService.ActivateAccount(token);

            if (result) CookiesExtensions.CreateCookie(CookieVariables.TempMessageCookie, "ActivateAccount.Submit.Success");

            return Redirect(loginPageUrl);
        }
    }
}