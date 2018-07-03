using PolRegio.Web.Filters;
using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace PolRegio.Web.Controllers.Account
{
    public class SocialMediaRegisterController : RenderMvcController
    {
        [GuestsOnly]
        public override ActionResult Index(RenderModel model)
        {
            return base.Index(model);
        }
    }
}