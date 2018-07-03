using Newtonsoft.Json;
using PolRegio.Domain.Models.Components;
using PolRegio.Domain.Models.Components.Contact;
using PolRegio.Domain.Models.Components.Home;
using PolRegio.Domain.Models.Database;
using PolRegio.Domain.Models.UmbracoCreate;
using PolRegio.Domain.Models.View.FilterState;
using PolRegio.Domain.Models.View.News;
using PolRegio.Domain.Services.Database;
using PolRegio.Domain.Services.News;
using PolRegio.Helpers.Constants;
using PolRegio.Helpers.Enums;
using PolRegio.Helpers.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Umbraco.Web;

namespace PolRegio.Services.News
{
    /// <summary>
    /// Klasa implementująca interfejs INewsService
    /// </summary>
    public class NewsService : INewsService
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
        public NewsService(IDBService dbService)
        {
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
            _dbService = dbService;
        }
        public NewsPageViewModel GetNewsBoxesModel(int currentUmbracoPageId, string typeFromUrl)
        {
            var _model = new NewsPageViewModel();
            _model.CurrentUmbracoPageId = currentUmbracoPageId;
            _model.CurrentPageCulture = Thread.CurrentThread.CurrentCulture;
            if (!string.IsNullOrEmpty(typeFromUrl))
            {
                _model.SelectedTypeFromUrl = typeFromUrl.Trim();
            }

            var _filterStateCookie = CookiesExtensions.GetCookieValue(CookieVariables.NewsFilterCookie);

            if (string.IsNullOrEmpty(_filterStateCookie)) return GetNewsBoxesModel(_model);

            var _filterModel = JsonConvert.DeserializeObject<NewsFilterStateViewModel>(StringExtensions.Base64Decode(_filterStateCookie));
            if (_filterModel == null) return GetNewsBoxesModel(_model);

            if (_filterModel.NewsRegionFiltr.HasValue)
                _model.SelectedRegionId = _filterModel.NewsRegionFiltr.Value;

            if (_filterModel.NewsTypeFilter != null && _filterModel.NewsTypeFilter.Count > 0)
            {
                _model.SelectedTypeIds = _filterModel.NewsTypeFilter;
            }

            return GetNewsBoxesModel(_model);
        }

        public NewsPageViewModel GetNewsBoxesModel(NewsPageViewModel model)
        {
            var _newsNode = _umbracoHelper.TypedContent(model.CurrentUmbracoPageId);
            var _newsList = _newsNode.Children.Where("Visible").Select(q => new ArticleWithDoubleFiltr(q));
            var _isParamFromUrl = !string.IsNullOrEmpty(model.SelectedTypeFromUrl);
            model.DisplayCount = new Information(_newsNode).DisplayItemsCount;

            #region Pobranie filtrów z bazy danych


            //Pobranie aktywnych filtrów z bazy danych
            var _regionFilterItemsFromDB = _dbService.GetAll<RegionDB>("PolRegioRegion", q => q.IsEnabled);
            var _newsTypeItemsFromDB = _dbService.GetAll<ArticleTypeDB>("PolRegioArticleType", q => q.IsEnabled);

            model.RegionFilter = _regionFilterItemsFromDB.Select(q => new FilterItem() { Id = q.Id, DisplayText = _umbracoHelper.GetDictionaryValue(q.DictionaryKey) }).ToList();
            model.RegionFilter.Insert(0, new FilterItem() { Id = 0, DisplayText = _umbracoHelper.GetDictionaryValue("News.Placeholder.AllRegions") });
            if (model.NewsTypeFilter == null)
            {
                model.NewsTypeFilter = _newsTypeItemsFromDB.Select(q => new CheckBoxFilterItem() { Id = q.Id, DisplayText = _umbracoHelper.GetDictionaryValue(q.DictionaryKey), IsChecked = (_isParamFromUrl && q.Name.ToLower() == model.SelectedTypeFromUrl.ToLower()) || (!_isParamFromUrl && model.SelectedTypeIds != null && model.SelectedTypeIds.Contains(q.Id)) }).ToList();
            }

            var _selectedTypesId = model.NewsTypeFilter.Where(q => q.IsChecked).Select(q => q.Id);

            #region SetFilterStateCookie

            var _typesId = _selectedTypesId as IList<int> ?? _selectedTypesId.ToList();
            var _filterModel = new NewsFilterStateViewModel()
            {
                NewsRegionFiltr = model.SelectedRegionId,
                NewsTypeFilter = _typesId.ToList()
            };
            CookiesExtensions.CreateCookie(CookieVariables.NewsFilterCookie, StringExtensions.Base64Encode(JsonConvert.SerializeObject(_filterModel)));
            #endregion
            #endregion
            if (model.SelectedRegionId != 0)
            {
                var _departmentNode = new OfficeAccordion(_newsNode.AncestorOrSelf(DocumentTypeEnum.location.ToString()).DescendantOrSelf(DocumentTypeEnum.officeAccordion.ToString()));

                if (_departmentNode != null && _departmentNode.AddOffice != null)
                {
                    var _contactRegionList = _departmentNode.AddOffice.Fieldsets.Where(x => x != null && x.Properties.Any() && !x.Disabled).Select(q => new RegionContactBox(q));
                    model.RegionContact = _contactRegionList.Where(q => q.Region != null).FirstOrDefault(q => q.Region.Key == model.SelectedRegionId.ToString());
                }

                _newsList = _newsList.Where(q => q.ArticleRegions.SavedValue != null && JsonConvert.DeserializeObject<IEnumerable<NuPickersSqlDropDownPicker>>(q.ArticleRegions.SavedValue.ToString()).Any(c => c.Key == model.SelectedRegionId.ToString()));
            }
            if (_typesId.Count() > 0)
            {
                _newsList = _newsList.Where(q => q.ArticleCategory.SavedValue != null && _typesId.Contains(int.Parse(JsonConvert.DeserializeObject<IEnumerable<NuPickersSqlDropDownPicker>>(q.ArticleCategory.SavedValue.ToString()).Select(c => c.Key).FirstOrDefault())));
            }

            model.AllNewsCount = _newsList.Count();
            model.NewsBoxesList = _newsList
                .OrderByDescending(q => q.ListArticleDate)
                .Take(model.DisplayCount)
                .Select(q => new NewsBoxModel(q));

            return model;
        }
        /// <summary>
        /// Metoda ładująca kolejene elementy na liście informacji
        /// </summary>
        /// <param name="selectedRegionId">wybrany region z dropdown</param>
        /// <param name="selectedTypeIds">wybrane typy informacji</param>
        /// <param name="skipCount">ilość elementów jaka jest na stronie</param>
        /// <param name="displayCount">ilość elementów jaką trzeba pobrać</param>
        /// <param name="currentPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>List obiektów NewsBoxModel</returns>
        public IEnumerable<NewsBoxModel> GetMoreNews(int selectedRegionId, List<int> selectedTypeIds, int skipCount, int displayCount, int currentPageId)
        {
            var _newsNode = _umbracoHelper.TypedContent(currentPageId);
            var _newsList = _newsNode.Children.Where("Visible").Select(q => new ArticleWithDoubleFiltr(q));

            if (selectedRegionId != 0)
            {
                _newsList = _newsList.Where(q => q.ArticleRegions.SavedValue != null && JsonConvert.DeserializeObject<IEnumerable<NuPickersSqlDropDownPicker>>(q.ArticleRegions.SavedValue.ToString()).Any(c => c.Key == selectedRegionId.ToString()));
            }
            if (selectedTypeIds != null && selectedTypeIds.Count() > 0)
            {
                _newsList = _newsList.Where(q => q.ArticleCategory.SavedValue != null && selectedTypeIds.Contains(int.Parse(JsonConvert.DeserializeObject<IEnumerable<NuPickersSqlDropDownPicker>>(q.ArticleCategory.SavedValue.ToString()).Select(c => c.Key).FirstOrDefault())));
            }
            #region Sortowanie po informacjach ogólnopolskich
            var _resultList = new List<NewsBoxModel>();
            var _allGroupNews = _newsList.GroupBy(q => q.ArticleRegions.SavedValue != null && JsonConvert.DeserializeObject<IEnumerable<NuPickersSqlDropDownPicker>>(q.ArticleRegions.SavedValue.ToString()).Any(c => c.Key == RegionVariables.all_poland_region_id));
            foreach (var item in _allGroupNews.OrderByDescending(q => q.Key))
            {
                _resultList.AddRange(item.OrderByDescending(q => q.ListArticleDate).Select(q => new NewsBoxModel(q)));
            }
            #endregion
            return _resultList.Skip(skipCount).Take(displayCount);
        }
    }
}
