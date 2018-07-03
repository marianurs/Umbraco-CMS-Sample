using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Models.Components.Search
{
    public class SearchItem
    {
        public string SearchTitle { get; set; }
        public string SearchUrl { get; set; }
        public string SearchBody { get; set; }
        public DateTime Date { get; set; }
    }
}
