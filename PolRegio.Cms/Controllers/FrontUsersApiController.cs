using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PolRegio.Domain.Models.Components.Account;
using PolRegio.Domain.Models.View.Account;
using PolRegio.Domain.Services.Account;
using Umbraco.Core;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;

namespace PolRegio.Cms.Controllers
{
    [PluginController("FrontUsersManager")]
    public class FrontUsersApiController : UmbracoAuthorizedJsonController
    {
        private readonly IAccountService _accountService;

        public FrontUsersApiController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public List<UserInfo> SearchUsers(string query)
        {
            return _accountService.SearchUsers(query);
        }

        public ProfileViewModel GetUser(int id)
        {
            return _accountService.GetProfileViewById(id);
        }

        [HttpPost]
        public ProfileResponse Update(int id, ProfileViewModel user)
        {
            return _accountService.EditProfile(id, user);
        }

        [HttpPost]
        public bool Delete(int id)
        {
            return _accountService.DeleteAccount(id);
        }
    }
}