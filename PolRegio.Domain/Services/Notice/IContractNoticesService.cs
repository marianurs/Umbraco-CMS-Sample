using PolRegio.Domain.Models.Components.Notice;
using PolRegio.Domain.Models.View.ContractNoticeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PolRegio.Domain.Services.Notice
{
    /// <summary>
    /// Interfejs zawierający metody do obsługi zamówień publicznych
    /// </summary>
    public interface IContractNoticesService
    {
        /// <summary>
        /// Metorda zwracająca klasę zawierającą elementy wyświetlane na stronie wszytskich zamówień publicznych
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>Model zawierający elementy strony ze wszytskimi zamówieniami publicznymi</returns>
        ContractNoticesViewModel GetNoticeBoxesModel(int currentUmbracoPageId, TempDataDictionary tempData);
        /// <summary>
        /// Metorda zwracająca klasę zawierającą elementy wyświetlane na stronie wszytskich zamówień publicznych
        /// </summary>
        /// <param name="model">Obiekt klasy ContractNoticesViewModel</param>
        /// <returns>Gotowy model do wyświetlenia na stronie wszystkich zamówień publicznych</returns>
        ContractNoticesViewModel GetNoticeBoxesModel(ContractNoticesViewModel model);
        /// <summary>
        /// Metoda ładująca kolejene elementy na liście wszytskich zamówień publicznych
        /// </summary>
        /// <param name="selectedAdministrativeId">wybrana jednostka administarcyjne z dropdown</param>
        /// <param name="selectedStatusId">wybrany status z dropdown</param>
        /// <param name="selectedLawActId">wybrany akt prawny z dropdown</param>
        /// <param name="selectedTypeOfContractId">wybrany rodzaj zamówienia z dropdown</param>
        /// <param name="startDate">data postępowania od</param>
        /// <param name="endDate">data postępowania do</param>
        /// <param name="name">nazwa postępowania</param>
        /// <param name="number">numer postępowania</param>
        /// <param name="skipCount">ilość elementów jaka jest na stronie</param>
        /// <param name="displayCount">ilość elementów jaką trzeba pobrać</param>
        /// <param name="currentPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>List obiektów NoticeBoxModel</returns>
        IEnumerable<NoticeBoxModel> GetMoreNotice(int selectedAdministrativeId, int selectedStatusId, int selectedLawActId, int selectedTypeOfContractId, DateTime? startDate, DateTime? endDate, string name, string number, int skipCount, int displayCount, int currentPageId);
        /// <summary>
        /// Metorda zwracająca klasę zawierającą elementy wyświetlane na stronie szczegółów zamówienia publicznego
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>Model zawierający szczegóły zamówienia publicznego</returns>
        ContractNoticeViewModel GetNoticeDetails(int currentUmbracoPageId);
    }
}
