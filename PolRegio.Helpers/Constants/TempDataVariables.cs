using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolRegio.Helpers.Constants
{
    /// <summary>
    /// Klasa zawierająca klucze wykorzystywane przy
    /// zapamiętywaniu filtrów na stronach z artykułami
    /// używane w TempData
    /// </summary>
    public static class TempDataVariables
    {
        #region Oferty regionalne
        /// <summary>
        /// Klucz do zapamiętania wybranego regionu na ofertach regionalnych
        /// </summary>
        public const string RegionOffersFiltr = "selected_region_offers_filtr";
        #endregion
        #region Informacje
        /// <summary>
        /// Klucz do zapamiętania wybranego regionu na informacjach
        /// </summary>
        public const string NewsRegionFiltr = "news_selected_region_filtr";
        /// <summary>
        /// Klucz do zapamiętania wybranego typu na informacjach
        /// </summary>
        public const string NewsTypeFiltr = "news_selected_type_filtr";
        #endregion
        #region Ogłoszenia o sprzedaży
        /// <summary>
        /// Klucz do zapamiętania wybranego statusu na ogłoszeniach o sprzedaży
        /// </summary>
        public const string AdvertisingOfSalesStatus = "advertising_selected_status";
        /// <summary>
        /// Klucz do zapamiętania wybranego sprzedającego na ogłoszeniach o sprzedaży
        /// </summary>
        public const string AdvertisingOfSalesAdministrative = "advertising_selected_administrative";
        /// <summary>
        /// Klucz do zapamiętania nazwy na ogłoszeniach o sprzedaży
        /// </summary>
        public const string AdvertisingOfSalesName = "advertising_selected_name";
        /// <summary>
        /// Klucz do zapamiętania numeru na ogłoszeniach o sprzedaży
        /// </summary>
        public const string AdvertisingOfSalesNumber = "advertising_selected_number";
        /// <summary>
        /// Klucz do zapamiętania daty od na ogłoszeniach o sprzedaży
        /// </summary>
        public const string AdvertisingOfSalesStartDate = "advertising_selected_start_date";
        /// <summary>
        /// Klucz do zapamiętania nazwy na ogłoszeniach o sprzedaży
        /// </summary>
        public const string AdvertisingOfSalesEndDate = "advertising_selected_end_date";
        #endregion
        #region Zamówienia publiczne
        /// <summary>
        /// Klucz do zapamiętania wybranego statusu na zamówieniach publicznych
        /// </summary>
        public const string ContractNoticesStatus = "contract_notices_selected_status";
        /// <summary>
        /// Klucz do zapamiętania wybranego sprzedającego na zamówieniach publicznych
        /// </summary>
        public const string ContractNoticesAdministrative = "contract_notices_selected_administrative";
        /// <summary>
        /// Klucz do zapamiętania nazwy na zamówieniach publicznych
        /// </summary>
        public const string ContractNoticesName = "contract_notices_selected_name";
        /// <summary>
        /// Klucz do zapamiętania numeru na zamówieniach publicznych
        /// </summary>
        public const string ContractNoticesNumber = "contract_notices_selected_number";
        /// <summary>
        /// Klucz do zapamiętania daty od na zamówieniach publicznych
        /// </summary>
        public const string ContractNoticesStartDate = "contract_notices_selected_start_date";
        /// <summary>
        /// Klucz do zapamiętania nazwy na zamówieniach publicznych
        /// </summary>
        public const string ContractNoticesEndDate = "contract_notices_selected_end_date";
        /// <summary>
        /// Klucz do zapamiętania wybranego aktu prawnego na zamówieniach publicznych
        /// </summary>
        public const string ContractNoticesLawAct = "contract_notices_selected_law_act";
        /// <summary>
        /// Klucz do zapamiętania wybranego rodzaju na zamówieniach publicznych
        /// </summary>
        public const string ContractNoticesType = "contract_notices_selected_type";
        #endregion
    }
}
