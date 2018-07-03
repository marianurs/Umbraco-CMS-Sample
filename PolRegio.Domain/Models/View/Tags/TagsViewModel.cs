using PolRegio.Domain.Models.Components.Home;
using PolRegio.Domain.Models.Components.Tag;
using PolRegio.Domain.Models.UmbracoCreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Models.View.Tags
{
    public class TagsViewModel
    {
        public ArticleListWithTag articleListWithTag { get; set; }
        /// <summary>
        /// Lista z informacjami wyświetlane na stronie ze wszytskimi artykułami po wybranym tagu
        /// </summary>
        public IList<TagItem> NewsBoxesList { get; set; }
        public string TagText { get; set; }
    }
}
