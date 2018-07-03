
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Models.Components.BipPage
{
    /// <summary>
    /// Klasa zawierająca elementy wyświetlane na liście BIP
    /// </summary>
    public class BipListItemModel
    {
        /// <summary>
        /// Adres url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Tytuł na liście
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Opis
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Data utworzenia
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
