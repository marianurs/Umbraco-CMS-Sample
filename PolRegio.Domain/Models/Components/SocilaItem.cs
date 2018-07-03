using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Models.Components
{
    /// <summary>
    /// Klasa opisująca element socialMedia
    /// </summary>
    public class SocilaItem
    {
        /// <summary>
        /// Nazwa Social Media
        /// </summary>
        public string SocialName { get; set; }
        /// <summary>
        /// Adres url, na który ma kierować element
        /// </summary>
        public string SocialUrl { get; set; }
    }
}
