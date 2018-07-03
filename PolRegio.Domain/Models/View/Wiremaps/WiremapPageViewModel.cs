using PolRegio.Domain.Models.Components;
using PolRegio.Domain.Models.UmbracoCreate;
using System.Collections.Generic;

namespace PolRegio.Domain.Models.View.Wiremaps
{
    /// <summary>
    /// Klasa zawierająca elementy wyświetlane na stronie mapa połączeń
    /// </summary>
    public class WiremapPageViewModel
    {
        /// <summary>
        /// Element mapy połączeń pobrane z bazy
        /// </summary>
        public Wiremap WiremapContent { get; set; }
        /// <summary>
        /// Lista dokumentów do pobrania 
        /// </summary
        public IEnumerable<DownloadItem> DownloadDocuments { get; set; }
    }
}
