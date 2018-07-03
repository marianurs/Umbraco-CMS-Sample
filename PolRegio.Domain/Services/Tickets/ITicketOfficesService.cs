using PolRegio.Domain.Models.Components.Ticket;
using PolRegio.Domain.Models.View.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Domain.Services.Tickets
{
    /// <summary>
    /// Interfejs zawierający metody do obsługi kas biletowych
    /// </summary>
    public interface ITicketOfficesService
    {
        /// <summary>
        /// Metorda zwracająca elementy wyświetlane na stronie wszytskich kas biletowych
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>Model zawierający elementy strony ze wszytskimi kasami biletowymi</returns>
        TicketOfficesPageViewModel GetTicketOfficesModel(int currentUmbracoPageId);
        /// <summary>
        /// Metorda zwracająca elementy wyświetlane na stronie wszytskich kas biletowych
        /// </summary>
        /// <param name="model">model TicketOfficesPageViewModel</param>
        /// <returns>Model zawierający elementy strony ze wszytskimi kasami biletowymi</returns>
        TicketOfficesPageViewModel GetTicketOfficesModel(TicketOfficesPageViewModel model);
        /// <summary>
        /// Metoda ładująca kolejene elementy na liście wszytskich kas biletowych
        /// </summary>
        /// <param name="officeName">nazwa kasy biletowej</param>
        /// <param name="skipCount">ilość elementów jaka jest na stronie</param>
        /// <param name="displayCount">ilość elementów jaką trzeba pobrać</param>
        /// <param name="currentPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>List obiektów TicketOfficeBoxModel</returns>
        IEnumerable<TicketOfficeBoxModel> GetMoreTicketOffices(string officeName, int skipCount, int displayCount, int currentPageId);
    }
}
