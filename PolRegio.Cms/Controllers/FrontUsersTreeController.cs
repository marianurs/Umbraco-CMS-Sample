using System;
using System.Linq;
using System.Net.Http.Formatting;
using umbraco;
using umbraco.BusinessLogic.Actions;
using Umbraco.Core;
using Umbraco.Core.Configuration;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;

namespace PolRegio.Cms.Controllers
{
    [Tree("users", "frontusermanage", "Użytkownicy serwisu")]
    [PluginController("FrontUsersManager")]
    public class FrontUsersTreeController : TreeController
    {
        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            var treeNodeCollection = new TreeNodeCollection
            {
                CreateTreeNode("1", "-1", queryStrings, "Zarządzaj użytkownikami", "icon-users", false,
                    "users/frontusermanage/index/1")
            };

            return treeNodeCollection;
        }

        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            var menuItemCollection = new MenuItemCollection();
            if (id != (-1).ToInvariantString())
                return null;
            menuItemCollection.Items.Add<RefreshNode, ActionRefresh>(ui.Text("actions", ActionRefresh.Instance.Alias),
                true);
            return menuItemCollection;
        }
    }
}