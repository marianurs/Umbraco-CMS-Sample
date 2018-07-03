using PolRegio.Domain.Models.View.Offers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Services.Offers
{
    /// <summary>
    /// Interfejs zawierający metody do obsługi strony oferty i promocje
    /// </summary>
    public interface IOffersService
    {
        /// <summary>
        /// Metoda zwracająca obiekt typu OffersPageViewModel zawierający 
        /// elementy wyświetlane na stronie oferty i promocje
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>Model zawierający elementy strony oferty i promocje</returns>
        OffersPageViewModel GetOffersPageViewModel(int currentUmbracoPageId);
    }
}
