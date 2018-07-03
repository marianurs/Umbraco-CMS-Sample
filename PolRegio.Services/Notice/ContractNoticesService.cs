using Newtonsoft.Json;
using PolRegio.Domain.Models.Components;
using PolRegio.Domain.Models.Components.Notice;
using PolRegio.Domain.Models.Database;
using PolRegio.Domain.Models.UmbracoCreate;
using PolRegio.Domain.Models.View.ContractNoticeModel;
using PolRegio.Domain.Models.View.FilterState;
using PolRegio.Domain.Services.Database;
using PolRegio.Domain.Services.Notice;
using PolRegio.Helpers.Constants;
using PolRegio.Helpers.Enums;
using PolRegio.Helpers.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using Umbraco.Web;

namespace PolRegio.Services.Notice
{
    /// <summary>
    /// Klasa implementująca interface IContractNoticesService
    /// </summary>
    public class ContractNoticesService : IContractNoticesService
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
        public ContractNoticesService(IDBService dbService)
        {
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
            _dbService = dbService;
        }
        /// <summary>
        /// Metorda zwracająca klasę zawierającą elementy wyświetlane na stronie wszytskich zamówień publicznych
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>Model zawierający elementy strony ze wszytskimi zamówieniami publicznymi</returns>
        public ContractNoticesViewModel GetNoticeBoxesModel(int currentUmbracoPageId, TempDataDictionary tempData)
        {
            var _model = new ContractNoticesViewModel();
            _model.CurrentUmbracoPageId = currentUmbracoPageId;
            _model.CurrentPageCulture = Thread.CurrentThread.CurrentCulture;

            var _filterStateCookie = CookiesExtensions.GetCookieValue(CookieVariables.ContractsFilterCookie);

            if (string.IsNullOrEmpty(_filterStateCookie)) return GetNoticeBoxesModel(_model);

            var _filterModel = JsonConvert.DeserializeObject<ContractsFilterStateModel>(StringExtensions.Base64Decode(_filterStateCookie));
            if (_filterModel == null) return GetNoticeBoxesModel(_model);

            _model.SelectedStatusId = _filterModel.SelectedStatusId;
            _model.SelectedAdministrativeId = _filterModel.SelectedAdministrativeId;
            _model.SelectedLawActId = _filterModel.SelectedLawActId;
            _model.SelectedTypeOfContractId = _filterModel.SelectedTypeOfContractId;

            if (!string.IsNullOrWhiteSpace(_filterModel.Name))
                _model.Name = _filterModel.Name;
            if (!string.IsNullOrWhiteSpace(_filterModel.Number))
                _model.Number = _filterModel.Number;
            if (_filterModel.StartDate.HasValue)
                _model.StartDate = _filterModel.StartDate.Value;
            if (_filterModel.EndDate.HasValue)
                _model.EndDate = _filterModel.EndDate.Value;

            return GetNoticeBoxesModel(_model);
        }
        /// <summary>
        /// Metorda zwracająca klasę zawierającą elementy wyświetlane na stronie wszytskich zamówień publicznych
        /// </summary>
        /// <param name="model">Obiekt klasy ContractNoticesViewModel</param>
        /// <returns>Gotowy model do wyświetlenia na stronie wszystkich zamówień publicznych</returns>
        public ContractNoticesViewModel GetNoticeBoxesModel(ContractNoticesViewModel model)
        {
            var _noticesNode = _umbracoHelper.TypedContent(model.CurrentUmbracoPageId);
            var _noticesList = _noticesNode.Children.Where("Visible").Select(q => new ContractNotice(q));
            var _noticesItem = new ContractNotices(_noticesNode);
            model.DisplayCount = _noticesItem.DisplayItemCount;
            model.ArchiveUrl = _noticesItem.ArchiveUrl;

            #region Pobranie filtrów z bazy danych
            //Pobranie aktywnych filtrów z bazy danych
            var _administrativeFilterItemsFromDB = _dbService.GetAll<AdministrativeDB>("PolRegioAdministrative", q => q.IsEnabled);

            model.AdministrativeFilter = _administrativeFilterItemsFromDB.Select(q => new FilterItem() { Id = q.Id, DisplayText = _umbracoHelper.GetDictionaryValue(q.DictionaryKey) }).ToList();
            model.AdministrativeFilter.Insert(0, new FilterItem() { Id = 0, DisplayText = _umbracoHelper.GetDictionaryValue("Notices.Placeholder.AllAdministrative") });

            model.StatusFilter = Enum.GetValues(typeof(NoticesSalesStatusEnum)).Cast<NoticesSalesStatusEnum>().Select(q => new FilterItem() { Id = (int)q, DisplayText = _umbracoHelper.GetDictionaryValue(string.Format("Advertising.Placeholder.{0}", q)) }).ToList();
            model.StatusFilter.Insert(0, new FilterItem() { Id = 0, DisplayText = _umbracoHelper.GetDictionaryValue("Notices.Placeholder.Any") });

            model.LawActFilter = Enum.GetValues(typeof(NoticesLawActEnum)).Cast<NoticesLawActEnum>().Select(q => new FilterItem() { Id = (int)q, DisplayText = _umbracoHelper.GetDictionaryValue(string.Format("Notices.LawAct.Placeholder.{0}", q)) }).ToList();
            model.LawActFilter.Insert(0, new FilterItem() { Id = 0, DisplayText = _umbracoHelper.GetDictionaryValue("Notices.Placeholder.Any") });

            model.TypeOfContractFilter = Enum.GetValues(typeof(NoticesTypeOfContractEnum)).Cast<NoticesTypeOfContractEnum>().Select(q => new FilterItem() { Id = (int)q, DisplayText = _umbracoHelper.GetDictionaryValue(string.Format("Notices.TypeOfContract.Placeholder.{0}", q)) }).ToList();
            model.TypeOfContractFilter.Insert(0, new FilterItem() { Id = 0, DisplayText = _umbracoHelper.GetDictionaryValue("Notices.Placeholder.Any") });

            #region SetFIlterStateCookie

            var _filterModel = new ContractsFilterStateModel()
            {
                SelectedStatusId = model.SelectedStatusId,
                SelectedAdministrativeId = model.SelectedAdministrativeId,
                Name = model.Name,
                Number = model.Number,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                SelectedLawActId = model.SelectedLawActId,
                SelectedTypeOfContractId = model.SelectedTypeOfContractId
            };
            CookiesExtensions.CreateCookie(CookieVariables.ContractsFilterCookie, StringExtensions.Base64Encode(JsonConvert.SerializeObject(_filterModel)));
            #endregion
            #endregion

            if (model.SelectedAdministrativeId != 0)
            {
                _noticesList = _noticesList.Where(q => q.Administrative.SavedValue != null && JsonConvert.DeserializeObject<IEnumerable<NuPickersSqlDropDownPicker>>(q.Administrative.SavedValue.ToString()).Any(c => c.Key == model.SelectedAdministrativeId.ToString()));
            }
            if (model.SelectedStatusId != 0)
            {
                _noticesList = _noticesList.Where(q => ((int)q.GetStatus()) == model.SelectedStatusId);
            }
            if (model.SelectedLawActId != 0)
            {
                _noticesList = _noticesList.Where(q => q.LawAct.SavedValue != null && JsonConvert.DeserializeObject<IEnumerable<NuPickersSqlDropDownPicker>>(q.LawAct.SavedValue.ToString()).Any(c => c.Key == model.SelectedLawActId.ToString()));
            }
            if (model.SelectedTypeOfContractId != 0)
            {
                _noticesList = _noticesList.Where(q => q.TypeOfContract.SavedValue != null && JsonConvert.DeserializeObject<IEnumerable<NuPickersSqlDropDownPicker>>(q.TypeOfContract.SavedValue.ToString()).Any(c => c.Key == model.SelectedTypeOfContractId.ToString()));
            }
            if (!string.IsNullOrEmpty(model.Name))
            {
                _noticesList = _noticesList.Where(q => q.Name.Trim().ToLower().Contains(model.Name.Trim().ToLower()));
            }
            if (!string.IsNullOrEmpty(model.Number))
            {
                _noticesList = _noticesList.Where(q => q.NumberOfProceedings.Trim().ToLower().Contains(model.Number.Trim().ToLower()));
            }
            if (model.StartDate.HasValue && model.EndDate.HasValue)
            {
                _noticesList = _noticesList.Where(q => q.InitiationOfProceedingsDate >= model.StartDate.Value && q.InitiationOfProceedingsDate <= model.EndDate.Value);
            }
            else if (model.StartDate.HasValue && !model.EndDate.HasValue)
            {
                _noticesList = _noticesList.Where(q => q.InitiationOfProceedingsDate >= model.StartDate.Value);
            }
            else if (!model.StartDate.HasValue && model.EndDate.HasValue)
            {
                _noticesList = _noticesList.Where(q => q.InitiationOfProceedingsDate <= model.EndDate.Value);
            }
            model.AllNewsCount = _noticesList.Count();
            model.NoticesList = _noticesList.OrderByDescending(q => q.InitiationOfProceedingsDate).Take(model.DisplayCount).Select(q => new NoticeBoxModel(q));
            return model;
        }
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
        public IEnumerable<NoticeBoxModel> GetMoreNotice(int selectedAdministrativeId, int selectedStatusId, int selectedLawActId, int selectedTypeOfContractId, DateTime? startDate, DateTime? endDate, string name, string number, int skipCount, int displayCount, int currentPageId)
        {
            var _noticesNode = _umbracoHelper.TypedContent(currentPageId);
            var _noticesList = _noticesNode.Children.Where("Visible").Select(q => new ContractNotice(q));

            if (selectedAdministrativeId != 0)
            {
                _noticesList = _noticesList.Where(q => q.Administrative.SavedValue != null && JsonConvert.DeserializeObject<IEnumerable<NuPickersSqlDropDownPicker>>(q.Administrative.SavedValue.ToString()).Any(c => c.Key == selectedAdministrativeId.ToString()));
            }
            if (selectedStatusId != 0)
            {
                _noticesList = _noticesList.Where(q => ((int)q.GetStatus()) == selectedStatusId);
            }
            if (selectedLawActId != 0)
            {
                _noticesList = _noticesList.Where(q => q.LawAct.SavedValue != null && JsonConvert.DeserializeObject<IEnumerable<NuPickersSqlDropDownPicker>>(q.LawAct.SavedValue.ToString()).Any(c => c.Key == selectedLawActId.ToString()));
            }
            if (selectedTypeOfContractId != 0)
            {
                _noticesList = _noticesList.Where(q => q.TypeOfContract.SavedValue != null && JsonConvert.DeserializeObject<IEnumerable<NuPickersSqlDropDownPicker>>(q.TypeOfContract.SavedValue.ToString()).Any(c => c.Key == selectedTypeOfContractId.ToString()));
            }
            if (!string.IsNullOrEmpty(name))
            {
                _noticesList = _noticesList.Where(q => q.Name.Trim().ToLower().Contains(name.Trim().ToLower()));
            }
            if (!string.IsNullOrEmpty(number))
            {
                _noticesList = _noticesList.Where(q => q.NumberOfProceedings.Trim().ToLower().Contains(number.Trim().ToLower()));
            }
            if (startDate.HasValue && endDate.HasValue)
            {
                _noticesList = _noticesList.Where(q => q.InitiationOfProceedingsDate >= startDate.Value && q.InitiationOfProceedingsDate <= endDate.Value);
            }
            else if (startDate.HasValue && !endDate.HasValue)
            {
                _noticesList = _noticesList.Where(q => q.InitiationOfProceedingsDate >= startDate.Value);
            }
            else if (!startDate.HasValue && endDate.HasValue)
            {
                _noticesList = _noticesList.Where(q => q.InitiationOfProceedingsDate <= endDate.Value);
            }
            return _noticesList.OrderByDescending(q => q.InitiationOfProceedingsDate).Skip(skipCount).Take(displayCount).Select(q => new NoticeBoxModel(q));
        }
        /// <summary>
        /// Metorda zwracająca klasę zawierającą elementy wyświetlane na stronie szczegółów zamówienia publicznego
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>Model zawierający szczegóły zamówienia publicznego</returns>
        public ContractNoticeViewModel GetNoticeDetails(int currentUmbracoPageId)
        {
            var _noticeNode = _umbracoHelper.TypedContent(currentUmbracoPageId);
            var _model = new ContractNoticeViewModel(_noticeNode);

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
