using PolRegio.Domain.Models.View.Wiremaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Services.Wiremaps
{
    /// <summary>
    /// Interfejs zawierający metody do obsługi strony z mapą połączeń
    /// </summary>
    public interface IWiremapService
    {
        /// <summary>
        /// Metoda zwracająca obiekt typu WiremapPageViewModel zawierający 
        /// elementy wyświetlane na stronie artykułu
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>Model zawierający elementy strony z artykułem</returns>
        WiremapPageViewModel GetWiremapPageViewModel(int currentUmbracoPageId);
    }
}
