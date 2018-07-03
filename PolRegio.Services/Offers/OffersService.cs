using PolRegio.Domain.Services.Offers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolRegio.Domain.Models.View.Offers;
using Umbraco.Web;
using PolRegio.Helpers.Enums;
using PolRegio.Domain.Models.Components.Offers;
using Newtonsoft.Json;
using PolRegio.Domain.Models.Components;
using PolRegio.Domain.Models.UmbracoCreate;

namespace PolRegio.Services.Offers
{
    /// <summary>
    /// Klasa implementująca interfejs IOffersService
    /// </summary>
    public class OffersService : IOffersService
    {
        /// <summary>
        /// Obiekt typu UmbracoHelper
        /// </summary>
        private UmbracoHelper _umbracoHelper;
        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        public OffersService()
        {
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
        }

        public OffersPageViewModel GetOffersPageViewModel(int currentUmbracoPageId)
        {
            var _model = new OffersPageViewModel();

            //Aktualna strona, na której się znajdujemy
            var _currentPage = _umbracoHelper.TypedContent(currentUmbracoPageId);
            var currPage = new OffersPromotions(_currentPage);
            //Aktualna lokalizacja, na której się znajdujemy
            var _localizationNode = _currentPage.AncestorOrSelf(DocumentTypeEnum.location.ToString());
            _model.OffersPageName = _currentPage.Name;
            _model.OffersBox = new List<OfferItem>();
            var _currentNodeChildren = _currentPage.Children.Where("Visible");

            foreach (var item in _currentPage.Children.Where("Visible"))
            {
                var typeItem = new OffersPromotions(item);
                var _offerItem = new OfferItem() { offersPageName = item.Name, Link = item.Url, pageCategory = Enum.Parse(typeof(OffersPageTypeEnum), JsonConvert.DeserializeObject<IEnumerable<NuPickersSqlDropDownPicker>>(typeItem.PageType.SavedValue.ToString()).FirstOrDefault().Key.ToString()).ToString() };
                _model.OffersBox.Add(_offerItem);
            }
            return _model;
        }
    }
}
