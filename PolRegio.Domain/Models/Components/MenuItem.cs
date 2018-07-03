using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Models.Components
{
    /// <summary>
    /// Klasa opisująca element w menu
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// Adres url, na który ma kierować element
        /// </summary>
        public Link Url { get; set; }
        /// <summary>
        /// Lista elementów podmenu
        /// </summary>
        public List<MenuItem> SubMenuItems { get; set; }
        /// <summary>
        /// Zwraca flagę czy dane element ma podmenu
        /// </summary>
        public bool HasChildren { get { return SubMenuItems != null && SubMenuItems.Count > 0; } }
    }
}
