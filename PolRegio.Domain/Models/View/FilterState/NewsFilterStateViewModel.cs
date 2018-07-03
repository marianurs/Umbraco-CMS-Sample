using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Models.View.FilterState
{
    public class NewsFilterStateViewModel
    {
        public int? NewsRegionFiltr { get; set; }
        public List<int> NewsTypeFilter { get; set; }
    }
}
