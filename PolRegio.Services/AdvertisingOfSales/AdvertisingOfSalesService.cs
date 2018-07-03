using Newtonsoft.Json;
using PolRegio.Domain.Models.Components;
using PolRegio.Domain.Models.Components.Advertising;
using PolRegio.Domain.Models.Database;
using PolRegio.Domain.Models.UmbracoCreate;
using PolRegio.Domain.Models.View.AdvertisingOfSalesModel;
using PolRegio.Domain.Models.View.FilterState;
using PolRegio.Domain.Services.AdvertisingOfSalesModel;
using PolRegio.Domain.Services.Database;
using PolRegio.Helpers.Constants;
using PolRegio.Helpers.Enums;
using PolRegio.Helpers.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using Umbraco.Web;

namespace PolRegio.Services.AdvertisingOfSalesModel
{
    /// <summary>
    /// Klasa implementująca interfejs IAdvertisingOfSalesService
    /// </summary>
    public class AdvertisingOfSalesService : IAdvertisingOfSalesService
    {
        /// <summary>
        /// Obiekt typu UmbracoHelper
        /// </summary>
        private UmbracoHelper _umbracoHelper;
        /// <summary>
        /// Obiekt typu IDBService
        /// </summary>
        private readonly IDBService _dbService;
        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        public AdvertisingOfSalesService(IDBService dbService)
        {
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
            _dbService = dbService;
        }

        /// <summary>
        /// Metorda zwracająca klasę zawierającą elementy wyświetlane na stronie wszytskich postępowań o sprzedaży
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>Model zawierający elementy strony ze wszytskimi postępowaniami o sprzedaży</returns>
        public AdvertisingOfSalesViewModel GetProcedureBoxesModel(int currentUmbracoPageId, TempDataDictionary tempData)
        {
            var _model = new AdvertisingOfSalesViewModel();
            _model.CurrentUmbracoPageId = currentUmbracoPageId;
            _model.CurrentPageCulture = Thread.CurrentThread.CurrentCulture;

            var _filterStateCookie = CookiesExtensions.GetCookieValue(CookieVariables.AdvertisingFilterCookie);

            if (string.IsNullOrEmpty(_filterStateCookie)) return GetProcedureBoxesModel(_model);

            var _filterModel = JsonConvert.DeserializeObject<AdvertisingFilterStateModel>(StringExtensions.Base64Decode(_filterStateCookie));
            if (_filterModel == null) return GetProcedureBoxesModel(_model);

            _model.SelectedStatusId = _filterModel.SelectedStatusId;
            _model.SelectedAdministrativeId = _filterModel.SelectedAdministrativeId;

            if (!string.IsNullOrWhiteSpace(_filterModel.Name))
                _model.Name = _filterModel.Name;
            if (!string.IsNullOrWhiteSpace(_filterModel.Number))
                _model.Number = _filterModel.Number;
            if (_filterModel.StartDate.HasValue)
                _model.StartDate = _filterModel.StartDate.Value;
            if (_filterModel.EndDate.HasValue)
                _model.EndDate = _filterModel.EndDate.Value;

            return GetProcedureBoxesModel(_model);
        }
        /// <summary>
        /// Metorda zwracająca klasę zawierającą elementy wyświetlane na stronie wszytskich postępowań o sprzedaży
        /// </summary>
        /// <param name="model">Obiekt klasy AdvertisingOfSalesViewModel</param>
        /// <returns>Gotowy model do wyświetlenia na stronie wszystkich postępowań o sprzedaży</returns>
        public AdvertisingOfSalesViewModel GetProcedureBoxesModel(AdvertisingOfSalesViewModel model)
        {
            var _advertisingNode = _umbracoHelper.TypedContent(model.CurrentUmbracoPageId);
            var _procedureList = _advertisingNode.Children.Where("Visible").Select(q => new AnnouncementOfSale(q));
            var _advertisingItem = new AdvertisingOfSales(_advertisingNode);

            model.DisplayCount = _advertisingItem.DisplayItemCount;
            model.ArchiveUrl = _advertisingItem.ArchiveUrl;

            #region Pobranie filtrów z bazy danych
            //Pobranie aktywnych filtrów z bazy danych
            var _administrativeFilterItemsFromDB = _dbService.GetAll<AdministrativeDB>("PolRegioAdministrative", q => q.IsEnabled);

            model.AdministrativeFilter = _administrativeFilterItemsFromDB.Select(q => new FilterItem() { Id = q.Id, DisplayText = _umbracoHelper.GetDictionaryValue(q.DictionaryKey) }).ToList();
            model.AdministrativeFilter.Insert(0, new FilterItem() { Id = 0, DisplayText = _umbracoHelper.GetDictionaryValue("Advertising.Placeholder.AllAdministrative") });

            model.StatusFilter = Enum.GetValues(typeof(NoticesSalesStatusEnum)).Cast<NoticesSalesStatusEnum>().Select(q => new FilterItem() { Id = (int)q, DisplayText = _umbracoHelper.GetDictionaryValue(string.Format("Advertising.Placeholder.{0}", q)) }).ToList();
            model.StatusFilter.Insert(0, new FilterItem() { Id = 0, DisplayText = _umbracoHelper.GetDictionaryValue("Advertising.Placeholder.Any") });
            #endregion
            #region SetFIlterStateCookie

            var _filterModel = new AdvertisingFilterStateModel()
            {
                SelectedStatusId = model.SelectedStatusId,
                SelectedAdministrativeId = model.SelectedAdministrativeId,
                Name = model.Name,
                Number = model.Number,
                StartDate = model.StartDate,
                EndDate = model.EndDate
            };
            CookiesExtensions.CreateCookie(CookieVariables.AdvertisingFilterCookie, StringExtensions.Base64Encode(JsonConvert.SerializeObject(_filterModel)));
            #endregion
            if (model.SelectedAdministrativeId != 0)
            {
                _procedureList = _procedureList.Where(q => q.Administrative.SavedValue != null && JsonConvert.DeserializeObject<IEnumerable<NuPickersSqlDropDownPicker>>(q.Administrative.SavedValue.ToString()).Any(c => c.Key == model.SelectedAdministrativeId.ToString()));
            }
            if (model.SelectedStatusId != 0)
            {
                _procedureList = _procedureList.Where(q => ((int)q.GetStatus()) == model.SelectedStatusId);
            }
            if (!string.IsNullOrEmpty(model.Name))
            {
                _procedureList = _procedureList.Where(q => q.Name.Trim().ToLower().Contains(model.Name.Trim().ToLower()));
            }
            if (!string.IsNullOrEmpty(model.Number))
            {
                _procedureList = _procedureList.Where(q => q.NumberOfProceedings.Trim().ToLower().Contains(model.Number.Trim().ToLower()));
            }
            if (model.StartDate.HasValue && model.EndDate.HasValue)
            {
                _procedureList = _procedureList.Where(q => q.InitiationOfProceedingsDate >= model.StartDate.Value && q.InitiationOfProceedingsDate <= model.EndDate.Value);
            }
            else if (model.StartDate.HasValue && !model.EndDate.HasValue)
            {
                _procedureList = _procedureList.Where(q => q.InitiationOfProceedingsDate >= model.StartDate.Value);
            }
            else if (!model.StartDate.HasValue && model.EndDate.HasValue)
            {
                _procedureList = _procedureList.Where(q => q.InitiationOfProceedingsDate <= model.EndDate.Value);
            }
            model.AllNewsCount = _procedureList.Count();
            model.ProcedureList = _procedureList.OrderByDescending(q => q.InitiationOfProceedingsDate).Take(model.DisplayCount).Select(q => new AdvertisingBoxModel(q));
            return model;

        }
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
        public IEnumerable<AdvertisingBoxModel> GetMoreProcedure(int selectedAdministrativeId, int selectedStatusId, DateTime? startDate, DateTime? endDate, string name, string number, int skipCount, int displayCount, int currentPageId)
        {
            var _advertisingNode = _umbracoHelper.TypedContent(currentPageId);
            var _procedureList = _advertisingNode.Children.Where("Visible").Select(q => new AnnouncementOfSale(q));

            if (selectedAdministrativeId != 0)
            {
                _procedureList = _procedureList.Where(q => q.Administrative.SavedValue != null && JsonConvert.DeserializeObject<IEnumerable<NuPickersSqlDropDownPicker>>(q.Administrative.SavedValue.ToString()).Any(c => c.Key == selectedAdministrativeId.ToString()));
            }
            if (selectedStatusId != 0)
            {
                _procedureList = _procedureList.Where(q => ((int)q.GetStatus()) == selectedStatusId);
            }
            if (!string.IsNullOrEmpty(name))
            {
                _procedureList = _procedureList.Where(q => q.Name.Trim().ToLower().Contains(name.Trim().ToLower()));
            }
            if (!string.IsNullOrEmpty(number))
            {
                _procedureList = _procedureList.Where(q => q.NumberOfProceedings.Trim().ToLower().Contains(number.Trim().ToLower()));
            }
            if (startDate.HasValue && endDate.HasValue)
            {
                _procedureList = _procedureList.Where(q => q.InitiationOfProceedingsDate >= startDate.Value && q.InitiationOfProceedingsDate <= endDate.Value);
            }
            else if (startDate.HasValue && !endDate.HasValue)
            {
                _procedureList = _procedureList.Where(q => q.InitiationOfProceedingsDate >= startDate.Value);
            }
            else if (!startDate.HasValue && endDate.HasValue)
            {
                _procedureList = _procedureList.Where(q => q.InitiationOfProceedingsDate <= endDate.Value);
            }
            return _procedureList.OrderByDescending(q => q.InitiationOfProceedingsDate).Skip(skipCount).Take(displayCount).Select(q => new AdvertisingBoxModel(q));

        }
        /// <summary>
        /// Metorda zwracająca klasę zawierającą elementy wyświetlane na stronie szczegółów postępowania o sprzedaży
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>Model zawierający szczegóły postępowania o sprzedaży</returns>
        public AnnouncementOfTheSaleViewModel GetProcedureDetails(int currentUmbracoPageId)
        {
            var _procedureNode = _umbracoHelper.TypedContent(currentUmbracoPageId);
            var _model = new AnnouncementOfTheSaleViewModel(_procedureNode);

            #region Dokumenty
            if (_model.AttachmentsList != null)
            {
                _model.DownloadDocuments = _model.AttachmentsList.Fieldsets.Where(x => x != null && x.Properties.Any() && !x.Disabled).Select(q => new DownloadItem()
                {
                    DocumentUrl = q.GetValue<string>("addDoc"),
                    DocumentName = q.GetValue<string>("articleDocName"),
                    DocumentDate = q.GetValue<DateTime>("chooseDate")
                });
            }
            #endregion

            return _model;
        }
    }
}
