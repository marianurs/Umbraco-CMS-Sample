using PolRegio.Domain.Models.UmbracoCreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace PolRegio.Domain.Models.Components.Ticket
{
    /// <summary>
    /// Klasa zawierająca elelemt kasy biletowej wytświetlanej na liście
    /// </summary>
    public class TicketOfficeBoxModel : TicketOffice
    {
        public TicketOfficeBoxModel(IPublishedContent content) : base(content)
        {
        }
        /// <summary>
        /// Url na który ma kierować box
        /// </summary>
        public string BoxUrl { get { return Content.Url; } }
    }
}
