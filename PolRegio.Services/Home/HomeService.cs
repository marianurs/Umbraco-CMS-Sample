using PolRegio.Domain.Services.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolRegio.Domain.Models.View.Home;
using Umbraco.Web;
using PolRegio.Domain.Models.UmbracoCreate;
using PolRegio.Domain.Models.Components.Home;
using PolRegio.Helpers.Enums;
using PolRegio.Domain.Models.View.SearchTicket;
using System.Threading;
using PolRegio.Domain.Services.SalesManago;

namespace PolRegio.Services.Home
{
    /// <summary>
    /// Klasa implementująca interfejs IHomeService
    /// </summary>
    public class HomeService : IHomeService
    {
        /// <summary>
        /// Obiekt typu UmbracoHelper
        /// </summary>
        private UmbracoHelper _umbracoHelper;

        private ISalesManagoRecommendedArticleService _salesManagoRecommendedArticle;
        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        public HomeService(ISalesManagoRecommendedArticleService salesManagoRecommendedArticle)
        {
            _salesManagoRecommendedArticle = salesManagoRecommendedArticle;
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
        }
        /// <summary>
        /// Metoda zwracająca obiekt typu HomePageViewModel zawierający 
        /// elementy wyświetlane na stronie głównej
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>Model zawierający elementy strony głównej</returns>
        public HomePageViewModel GetHomePageViewModel(int currentUmbracoPageId)
        {
            var _model = new HomePageViewModel();
            _model.SearchTicketModel = new SearchTicketFormView() { CurrentPageCulture = Thread.CurrentThread.CurrentCulture };
            //Aktualna strona, na której się znajdujemy
            var _currentPage = _umbracoHelper.TypedContent(currentUmbracoPageId);

            _model.HomePageFromDb = new Location(_currentPage);
            var _newsPage = _currentPage.DescendantOrSelf(DocumentTypeEnum.Information.ToString());
            if (_newsPage != null)
            {
                _model.AllNewsUrl = _newsPage.Url;
            }
            var _offersPage = _currentPage.DescendantOrSelf(DocumentTypeEnum.OffersAndPromotions.ToString());
            if (_offersPage != null)
            {
                _model.AllOffersUrl = _offersPage.Url;
            }
            //Pobranie ofert i promocji na stronie głównej
            if (!string.IsNullOrEmpty(_model.HomePageFromDb.Offers))
            {
                var _selectedOffersIds = _model.HomePageFromDb.Offers.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
                var salesmanagoOffersIds = _salesManagoRecommendedArticle.GetOffersIdForCurrentUser();

                _model.OffersListDisplayOnHomePage = _selectedOffersIds.Take(2)
                    .Union(salesmanagoOffersIds)
                    .Union(_selectedOffersIds.Skip(2))
                    .Distinct()
                    .Select(_umbracoHelper.TypedContent)
                    .Where(x => x != null)
                    .Select(q => new OfferBoxModel(q))
                    .Take(4)
                    .ToList();
            }

            //Pobranie news na stronie głównej
            if (!string.IsNullOrEmpty(_model.HomePageFromDb.News))
            {
                var _selectedNewsIds = _model.HomePageFromDb.News.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
                _model.NewsListDisplayOnHomePage = _umbracoHelper.TypedContent(_selectedNewsIds).Where(x => x != null).Select(q => new NewsBoxModel(q));
            }

            //pobieranie bannera
            if (_model.HomePageFromDb.GetPropertyValue<bool>("BannerActive"))
            {
                _model.Baner = new BanerViewModel()
                {
                    DesktopImageUrl = _model.HomePageFromDb.GetPropertyValue<string>("BannerPictureDesktop"),
                    MobileImageUrl = _model.HomePageFromDb.GetPropertyValue<string>("BannerPictureMobile"),
                    ImageAlt = _model.HomePageFromDb.GetPropertyValue<string>("BannerPictureAlt"),

                    ButtonUrl = _model.HomePageFromDb.GetPropertyValue<string>("BannerButtonUrl"),
                    ButtonIsNewTab = _model.HomePageFromDb.GetPropertyValue<bool>("BannerButtonNewTab"),
                };
            }

            //slider
            {
                var slider = _currentPage
                    .Children
                    .Where(c => c.DocumentTypeAlias == "slider")
                    .Where(c => c.IsVisible())
                    .Select(c=> new Slider(c))
                    .FirstOrDefault();

                if (slider != null)
                    _model.SliderSlides = slider.Children
                        .Select(c => new SliderItem(c))
                        .Where(s => s != null && s.IsVisible())
                        .ToList();
                else _model.SliderSlides = new List<SliderItem>();
            }

            return _model;
        }
    }
}
