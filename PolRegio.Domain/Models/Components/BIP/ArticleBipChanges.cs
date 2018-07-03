using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Models.Components.BIP
{
    public class ArticleBipChanges
    {
        public DateTime ChangesDate { get; set; }
        public string UserName { get; set; }
        public string ChangeType { get; set; }
        public string ChangeDescription { get; set; }

    }
}
