using PolRegio.Domain.Models.Components.Advertising;
using PolRegio.Domain.Models.UmbracoCreate;
using PolRegio.Domain.Models.View.AdvertisingOfSalesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PolRegio.Domain.Services.AdvertisingOfSalesModel
{
    /// <summary>
    /// Interfejs zawierający metody do obsługi postępowań o sprzedaży
    /// </summary>
    public interface IAdvertisingOfSalesService
    {
        /// <summary>
        /// Metorda zwracająca klasę zawierającą elementy wyświetlane na stronie wszytskich postępowań o sprzedaży
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>Model zawierający elementy strony ze wszytskimi postępowaniami o sprzedaży</returns>
        AdvertisingOfSalesViewModel GetProcedureBoxesModel(int currentUmbracoPageId, TempDataDictionary tempData);
        /// <summary>
        /// Metorda zwracająca klasę zawierającą elementy wyświetlane na stronie wszytskich postępowań o sprzedaży
        /// </summary>
        /// <param name="model">Obiekt klasy AdvertisingOfSalesViewModel</param>
        /// <returns>Gotowy model do wyświetlenia na stronie wszystkich postępowań o sprzedaży</returns>
        AdvertisingOfSalesViewModel GetProcedureBoxesModel(AdvertisingOfSalesViewModel model);
        /// <summary>
        /// Metoda ładująca kolejene elementy na liście wszytskich postępowań o sprzedaży
        /// </summary>
        /// <param name="selectedAdministrativeId">wybrana jednostka administarcyjne z dropdown</param>
        /// <param name="selectedStatusId">wybrany status z dropdown</param>
        /// <param name="startDate">data postępowania od</param>
        /// <param name="endDate">data postępowania do</param>
        /// <param name="name">nazwa postępowania</param>
        /// <param name="number">numer postępowania</param>
        /// <param name="skipCount">ilość elementów jaka jest na stronie</param>
        /// <param name="displayCount">ilość elementów jaką trzeba pobrać</param>
        /// <param name="currentPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>List obiektów AdvertisingBoxModel</returns>
        IEnumerable<AdvertisingBoxModel> GetMoreProcedure(int selectedAdministrativeId, int selectedStatusId, DateTime? startDate, DateTime? endDate, string name, string number, int skipCount, int displayCount, int currentPageId);
        /// <summary>
        /// Metorda zwracająca klasę zawierającą elementy wyświetlane na stronie szczegółów postępowania o sprzedaży
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>Model zawierający szczegóły postępowania o sprzedaży</returns>
        AnnouncementOfTheSaleViewModel GetProcedureDetails(int currentUmbracoPageId);
    }
}
