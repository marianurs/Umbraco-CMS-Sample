using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Models.Components
{
    /// <summary>
    /// Klasa zawierająca dokument do pobrania na stronie artykułu
    /// </summary>
    public class DownloadItem
    {
        /// <summary>
        /// Nazwa dokumentu
        /// </summary>
        public string DocumentName { get; set; }
        /// <summary>
        /// Link do dokumentu
        /// </summary>
        public string DocumentUrl { get; set; }
        /// <summary>
        /// Data wstawienia dokumentu
        /// </summary>
        public DateTime DocumentDate { get; set; }
    }
}
