using PolRegio.Domain.Models.View.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Services.Home
{
    /// <summary>
    /// Interfejs zawierający metody do obsługi home page
    /// </summary>
    public interface IHomeService
    {
        /// <summary>
        /// Metoda zwracająca obiekt typu HomePageViewModel zawierający 
        /// elementy wyświetlane na stronie głównej
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>Model zawierający elementy strony głównej</returns>
        HomePageViewModel GetHomePageViewModel(int currentUmbracoPageId);
    }
}
