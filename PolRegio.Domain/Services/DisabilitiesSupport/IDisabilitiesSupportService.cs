using PolRegio.Domain.Models.View.DisabilitiesSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Services.DisabilitiesSupport
{
    /// <summary>
    /// Interfejs zawierający obsługę osób niepełnosprawnych
    /// </summary>
    public interface IDisabilitiesSupportService
    {
        /// <summary>
        /// Metoda zwracająca obiekt typu DisabilitiesSupportViewModel zawierający 
        /// elementy wyświetlane na stronie obsługa osób niepełnosprawnych
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns></returns>
        DisabilitiesSupportViewModel GetDisabilitiesSupportVieww(int currentUmbracoPageId);
    }
}
