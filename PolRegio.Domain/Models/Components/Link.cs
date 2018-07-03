using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Models.Components
{
    /// <summary>
    /// Klasa pomocnicza opisująca url link
    /// </summary>
    public class Link
    {
        /// <summary>
        /// Tekst linku
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Adres url linku
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Określenie czy link jest aktywny
        /// dopuszczalne wartości true/false
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Określenie czy link ma się otwierać w nowym oknie
        /// dopuszczalne wartości true/false
        /// </summary>
        public bool OpenInNewWindow { get; set; }
        /// <summary>
        /// Zwraca flagę czy obiekt ma docelowy url
        /// </summary>
        public bool HasUrl { get { return !string.IsNullOrEmpty(Url); } }
        /// <summary>
        /// Dodatkowy atrybut w celu usunięcia zapamiętanych filtrów na stronie
        /// </summary>
        public string DataCookie { get; set; }
    }
}
