using PolRegio.Cms.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using umbraco;
using umbraco.BusinessLogic.Actions;
using Umbraco.Core;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;

namespace PolRegio.Cms.Trees
{
    [Tree("additionalsettings", "administrativedbtree", "Administrative")]
    [PluginController("AdditionalSettings")]
    public class AdministrativeDBTreeController : TreeController
    {
        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            var menu = new MenuItemCollection();

            if (id == Constants.System.Root.ToInvariantString())
            {
                // root actions              
                menu.Items.Add<CreateChildEntity, ActionNew>(ui.Text("actions", ActionNew.Instance.Alias));
                menu.Items.Add<RefreshNode, ActionRefresh>(ui.Text("actions", ActionRefresh.Instance.Alias), true);
                //menu.Items.Add<RefreshNode, ActionSort>(ui.Text("actions", ActionSort.Instance.Alias), true);
                return menu;
            }
            return menu;
        }

        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            if (id == Constants.System.Root.ToInvariantString())
            {
                var ctrl = new AdministrativeDBApiController();
                var nodes = new TreeNodeCollection();

                foreach (var item in ctrl.GetAll())
                {
                    var node = CreateTreeNode(
                        item.Id.ToString(),
                        "-1",
                        queryStrings,
                        item.ToString(),
                        "icon-settings-alt",
                        false);

                    nodes.Add(node);

                }
                return nodes;
            }

            //this tree doesn't suport rendering more than 1 level
            throw new NotSupportedException();
        }
    }
}
