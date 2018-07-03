using PolRegio.Helpers.Constants;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PolRegio.Web.Filters
{
    public class PolRegioAuthorizationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.IsChildAction)
                return;

            var action = filterContext.ActionDescriptor;
            var context = filterContext.HttpContext;
            var language = GetLanguage(context.Request);

            var loginUrl = AccountRedirectUrls.LoginPage(language);
            var homeUrl = AccountRedirectUrls.Home(language);

            if (HasAttribute<AuthorizedOnlyAttribute>(action) && !IsAuthenticated(context))
                filterContext.Result = new RedirectResult(loginUrl);

            if (HasAttribute<GuestsOnlyAttribute>(action) && IsAuthenticated(context))
                filterContext.Result = new RedirectResult(homeUrl);
        }

        private bool IsAuthenticated(HttpContextBase context)
        {
            return context.Request.IsAuthenticated
                && context.User.Identity.AuthenticationType == "Forms";
        }

        private bool HasAttribute<TAttribute>(ActionDescriptor action)
        {
            var attributes = action.GetCustomAttributes(typeof(TAttribute), inherit: true);
            return attributes != null && attributes.Any();
        }

        private string GetLanguage(HttpRequestBase request)
        {
            var urlParts = request.Path.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            return urlParts.FirstOrDefault();
        }
    }
}