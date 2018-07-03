using PolRegio.Domain.Models.ISS;
using PolRegio.Domain.Models.View.SearchTicket;
using PolRegio.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace PolRegio.Domain.Services.ISS
{
    /// <summary>
    /// Interface zawierający metody dotyczące pobierania i wysyłania danych do systemu ISS
    /// </summary>
    public interface IISSApiService
    {
        /// <summary>
        /// Metoda zwracająca pobrane elementy z ISS Api jako Json
        /// </summary>
        /// <param name="type">typ elementu jaki chcemy pobrać</param>
        /// <returns>string zawierający Json</returns>
        string GetAdditionalInformationForType();

        /// <summary>
        /// Metoda zwracająca pobrane elementy z ISS Api jako Json
        /// </summary>
        /// <param name="type">typ elementu jaki chcemy pobrać</param>
        /// <returns>string zawierający Json</returns>
        IEnumerable<T> GetAdditionalInformationForType<T>() where T : class;

        void GetAnswerKeyToRedirectToBuyTicket(ref SearchTicketFormView model);

        /// <summary>
        /// metoda aktualizuje listę stacji w bazie danych 
        /// </summary>
        void UpdateStationsInDB();
    }
}
