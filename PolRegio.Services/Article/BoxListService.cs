using PolRegio.Domain.Services.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolRegio.Domain.Models.Components.Home;
using PolRegio.Domain.Models.View.Article;
using Umbraco.Web;
using System.Threading;
using PolRegio.Domain.Models.UmbracoCreate;
using Newtonsoft.Json;
using PolRegio.Domain.Models.Components;
using PolRegio.Domain.Models.Database;
using PolRegio.Domain.Models.View.FilterState;
using PolRegio.Domain.Services.Database;
using PolRegio.Helpers.Constants;
using PolRegio.Helpers.Extensions;

namespace PolRegio.Services.Article
{
    /// <summary>
    /// Klasa implementująca interface IBoxListService
    /// </summary>
    public class BoxListService : IBoxListService
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
        public BoxListService(IDBService dbService)
        {
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
            _dbService = dbService;
        }
        /// <summary>
        /// Metorda zwracająca klasę zawierającą elementy wyświetlane na stronie wszytskich artykułów
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>Model zawierający elementy strony ze wszytskimi artykułami</returns>
        public ArticleBoxViewModel GetArticleBoxesModel(int currentUmbracoPageId)
        {
            var _model = new ArticleBoxViewModel();
            _model.CurrentUmbracoPageId = currentUmbracoPageId;
            _model.CurrentPageCulture = Thread.CurrentThread.CurrentCulture;

            return GetArticleBoxesModel(_model);
        }
        /// <summary>
        /// Metorda zwracająca klasę zawierającą elementy wyświetlane na stronie wszytskich artykułów
        /// </summary>
        /// <param name="model">Obiekt klasy ArticleBoxViewModel</param>
        /// <returns>Gotowy model do wyświetlenia na stronie wszystkich artykułów</returns>
        public ArticleBoxViewModel GetArticleBoxesModel(ArticleBoxViewModel model)
        {
            var _offersNode = _umbracoHelper.TypedContent(model.CurrentUmbracoPageId);

            var _articleList = _offersNode.Children.Where("Visible");
            model.DisplayCount = new OffersPromotions(_offersNode).DisplayCount;

            model.AllNewsCount = _articleList.Count();
            model.ArticleList = _articleList.Take(model.DisplayCount).Select(q => new OfferBoxModel(q));

            return model;
        }
        /// <summary>
        /// Metoda ładująca kolejene elementy na liście informacji
        /// </summary>
        /// <param name="skipCount">ilość elementów jaka jest na stronie</param>
        /// <param name="displayCount">ilość elementów jaką trzeba pobrać</param>
        /// <param name="currentPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>List obiektów OfferBoxModel</returns>
        public IEnumerable<OfferBoxModel> GetMoreArticle(int skipCount, int displayCount, int currentPageId)
        {
            var _offersNode = _umbracoHelper.TypedContent(currentPageId);
            var _articleList = _offersNode.Children.Where("Visible");
            return _articleList.Skip(skipCount).Take(displayCount).Select(q => new OfferBoxModel(q));
        }
        /// <summary>
        /// Metorda zwracająca klasę zawierającą elementy wyświetlane na stronie wszytskich ofert regionalnych
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>Model zawierający elementy strony ze wszytskimi artykułami</returns>
        public RegionalOfferBoxViewModel GetRegionalArticleBoxesModel(int currentUmbracoPageId)
        {
            var _model = new RegionalOfferBoxViewModel();
            _model.CurrentUmbracoPageId = currentUmbracoPageId;
            _model.CurrentPageCulture = Thread.CurrentThread.CurrentCulture;

            var _filterStateCookie = CookiesExtensions.GetCookieValue(CookieVariables.OffersFilterCookie);

            if (string.IsNullOrEmpty(_filterStateCookie)) return GetRegionalArticleBoxesModel(_model);

            var _filterModel = JsonConvert.DeserializeObject<NewsFilterStateViewModel>(StringExtensions.Base64Decode(_filterStateCookie));
            if (_filterModel == null) return GetRegionalArticleBoxesModel(_model);

            if (_filterModel.NewsRegionFiltr.HasValue)
                _model.SelectedRegionId = _filterModel.NewsRegionFiltr.Value;

            return GetRegionalArticleBoxesModel(_model);
        }
        /// <summary>
        /// Metorda zwracająca klasę zawierającą elementy wyświetlane na stronie wszytskich ofert regionalnych
        /// </summary>
        /// <param name="model">Obiekt klasy RegionalOfferBoxViewModel</param>
        /// <returns>Gotowy model do wyświetlenia na stronie wszystkich artykułów</returns>
        public RegionalOfferBoxViewModel GetRegionalArticleBoxesModel(RegionalOfferBoxViewModel model)
        {
            var _offersNode = _umbracoHelper.TypedContent(model.CurrentUmbracoPageId);

            var _articleList = _offersNode.Children.Where("Visible").Select(q => new ArticleWithFilter(q));
            model.DisplayCount = new RegionalOffers(_offersNode).DisplayCount;

            #region Pobranie filtrów z bazy danych
            //Pobranie aktywnych filtrów z bazy danych
            var _regionFilterItemsFromDB = _dbService.GetAll<RegionDB>("PolRegioRegion", q => q.IsEnabled);

            model.RegionFilter = _regionFilterItemsFromDB.Where(q => q.Id != int.Parse(RegionVariables.all_poland_region_id)).Select(q => new FilterItem() { Id = q.Id, DisplayText = _umbracoHelper.GetDictionaryValue(q.DictionaryKey) }).ToList();
            model.RegionFilter.Insert(0, new FilterItem() { Id = 0, DisplayText = _umbracoHelper.GetDictionaryValue("News.Placeholder.AllRegions") });

            var _filterModel = new NewsFilterStateViewModel()
            {
                NewsRegionFiltr = model.SelectedRegionId
            };
            CookiesExtensions.CreateCookie(CookieVariables.OffersFilterCookie, StringExtensions.Base64Encode(JsonConvert.SerializeObject(_filterModel)));

            #endregion
            if (model.SelectedRegionId != 0)
            {
                _articleList = _articleList.Where(q => q.RegionFiltr.SavedValue != null && JsonConvert.DeserializeObject<IEnumerable<NuPickersSqlDropDownPicker>>(q.RegionFiltr.SavedValue.ToString()).Any(c => c.Key == model.SelectedRegionId.ToString()));
            }

            model.AllNewsCount = _articleList.Count();
            model.ArticleList = _articleList.Take(model.DisplayCount).Select(q => new OfferBoxModel(q));

            return model;
        }
        /// <summary>
        /// Metoda ładująca kolejene elementy na liście ofert regionalnych
        /// </summary>
        /// <param name="selectedRegionId">wybrany region z dropdown</param>
        /// <param name="skipCount">ilość elementów jaka jest na stronie</param>
        /// <param name="displayCount">ilość elementów jaką trzeba pobrać</param>
        /// <param name="currentPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>List obiektów OfferBoxModel</returns>
        public IEnumerable<OfferBoxModel> GetMoreRegionalArticle(int selectedRegionId, int skipCount, int displayCount, int currentPageId)
        {
            var _offersNode = _umbracoHelper.TypedContent(currentPageId);
            var _articleList = _offersNode.Children.Where("Visible").Select(q => new ArticleWithFilter(q));

            if (selectedRegionId != 0)
            {
                _articleList = _articleList.Where(q => q.RegionFiltr.SavedValue != null && JsonConvert.DeserializeObject<IEnumerable<NuPickersSqlDropDownPicker>>(q.RegionFiltr.SavedValue.ToString()).Any(c => c.Key == selectedRegionId.ToString()));
            }
            return _articleList.Skip(skipCount).Take(displayCount).Select(q => new OfferBoxModel(q));
        }
    }
}
