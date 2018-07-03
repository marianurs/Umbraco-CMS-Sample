using System;
using Archetype.Models;
using Newtonsoft.Json.Linq;
using PolRegio.Domain.Models.Components;
using PolRegio.Domain.Models.Database;
using PolRegio.Domain.Models.UmbracoCreate;
using PolRegio.Domain.Models.View.Layout;
using PolRegio.Domain.Services.Account;
using PolRegio.Domain.Services.Database;
using PolRegio.Domain.Services.Layout;
using PolRegio.Domain.Services.Shared;
using PolRegio.Helpers.Constants;
using PolRegio.Helpers.Enums;
using System.Collections.Generic;
using System.Linq;
using PolRegio.Domain.Models.View.Account;
using PolRegio.Domain.Models.View.Home;
using umbraco;
using umbraco.NodeFactory;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace PolRegio.Services.Layout
{
    /// <summary>
    /// klasa implementująca interfejs zawierający generowanie headera i footera na stronie
    /// </summary>
    public class LayoutService : ILayoutService
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
        /// Obiekt typu IUmbracoHelperService
        /// </summary>
        private readonly IUmbracoHelperService _umbHelperService;

        private readonly IAccountService _accountService;
        /// <summary>
        /// Konstruktor klasy LayoutService
        /// </summary>
        public LayoutService(IDBService dbService, IUmbracoHelperService umbHelperService, IAccountService accountService)
        {
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
            _dbService = dbService;
            _umbHelperService = umbHelperService;
            _accountService = accountService;
        }
        /// <summary>
        /// Metoda zwracająca obiekt typu HeaderViewModel
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>obiekt klasy HeaderViewModel</returns>
        public HeaderViewModel GetHeaderViewModel(int currentUmbracoPageId)
        {
            var _model = new HeaderViewModel();
            //Aktualna strona, na której się znajdujemy
            var _currentPage = _umbracoHelper.TypedContent(currentUmbracoPageId);
            //Aktualna lokalizacja, na której się znajdujemy
            var _localizationNode = _currentPage.AncestorOrSelf(DocumentTypeEnum.location.ToString());

            _model.Languages = _localizationNode.Parent.Children
                .Where(x => x.DocumentTypeAlias == DocumentTypeEnum.location.ToString())
                .Where(NodeAtributeEnum.Visible.ToString())
                .Select(q => new LangLink()
                {
                    IsActive = q.Id == _localizationNode.Id,
                    Url = q.Url,
                    Text = q.Name,
                    LanguageCode = _umbHelperService.GetCulture(q).TwoLetterISOLanguageName
                });
            
            var _localization = new Location(_localizationNode);
            _model.HomePageUrl = _localizationNode.Url;
            _model.MenuItems = new List<MenuItem>();
            _model.HelplineNumber = _localization.HelplineNumber;
            _model.HelplineTooltip = _localization.HelplineTooltipText;

            _model.CookieInformation = new Cookies(_localizationNode);
            foreach (var _localizationChild in _localizationNode.Children.Where("Visible"))
            {
                var _item = new SelectMenuOrFooter(_localizationChild);
                if (_item.ChoiceMenuOrFooter != null && _item.ChoiceMenuOrFooter.ToString() == MenuPositionTypeEnum.Menu.ToString())
                {
                    //Level 1
                    var itemName = _localizationChild.Name;
                    if (_localizationChild.DocumentTypeAlias == DocumentTypeEnum.account.ToString())
                        itemName = _accountService.IsAuthenticated()
                            ? _localizationChild.GetPropertyValue<string>("loggedInMenuText")
                            : _localizationChild.GetPropertyValue<string>("guestMenuText");

                    var _menuItem = new MenuItem();
                    _menuItem.Url = new Link() { Text = itemName, IsActive = true };

                    var _currentNodeChildren = _localizationChild.Children.Where("Visible");
                    if (_currentNodeChildren != null && _currentNodeChildren.Count() > 0)
                    {
                        //level 2
                        _menuItem.SubMenuItems = new List<MenuItem>();
                        foreach (var item in _currentNodeChildren)
                        {
                            if (ItemShouldNotBeDisplayed(item))
                                continue;

                            var _subMenuItem = new MenuItem();
                            var _subMenuChildren = item.Children.Where("Visible");
                            var _hasSubMenu = _localizationChild.DocumentTypeAlias == DocumentTypeEnum.ForTravelers.ToString() && _subMenuChildren.Count() > 0;
                            _subMenuItem.Url = new Link() { IsActive = true, Text = item.Name, Url = _hasSubMenu ? string.Empty : item.Url, DataCookie = item.IsDocumentType(RegionalOffers.ModelTypeAlias) ? CookieVariables.OffersFilterCookie : "" };
                            if (_hasSubMenu)
                            {
                                if (item.DocumentTypeAlias == DocumentTypeEnum.Information.ToString())
                                {
                                    var _newsTypeItemsFromDB = _dbService.GetAll<ArticleTypeDB>("PolRegioArticleType", q => q.IsEnabled);
                                    _subMenuItem.SubMenuItems = _newsTypeItemsFromDB.Select(q => new MenuItem()
                                    {
                                        Url = new Link()
                                        {
                                            IsActive = true,
                                            Url = string.Format("{0}?type={1}", item.Url, q.Name.ToLower()),
                                            Text = _umbracoHelper.GetDictionaryValue(q.DictionaryKey),
                                            DataCookie = CookieVariables.NewsFilterCookie
                                        }
                                    }).ToList();
                                    _subMenuItem.SubMenuItems.Insert(0, new MenuItem() { Url = new Link() { IsActive = true, Text = _umbracoHelper.GetDictionaryValue("Menu.Link.SeeAllInformation"), Url = item.Url, DataCookie = CookieVariables.NewsFilterCookie } });
                                }
                                else
                                {
                                    _subMenuItem.SubMenuItems = new List<MenuItem>();
                                    foreach (var itemLinks in _subMenuChildren)
                                    {
                                        if (itemLinks.DocumentTypeAlias == DocumentTypeEnum.searchConnection.ToString())
                                        {
                                            foreach (var itemLink in itemLinks.GetPropertyValue<JArray>("link"))
                                            {
                                                MenuItem _footerItem = new MenuItem()
                                                {
                                                    Url = new Link()
                                                    {
                                                        IsActive = true,
                                                        Url = itemLink.Value<string>("link"),
                                                        OpenInNewWindow = itemLink.Value<bool>("newWindow")
                                                    }
                                                };
                                                _footerItem.Url.Text = itemLinks.Name;
                                                _subMenuItem.SubMenuItems.Add(_footerItem);
                                            }
                                        }
                                        else
                                        {
                                            var _menuSubItem = new MenuItem()
                                            {
                                                Url = new Link()
                                                {
                                                    IsActive = true,
                                                    Url = itemLinks.Url,
                                                    Text = itemLinks.Name
                                                }
                                            };
                                            _subMenuItem.SubMenuItems.Add(_menuSubItem);
                                        }
                                    }
                                }
                            }

                            _menuItem.SubMenuItems.Add(_subMenuItem);
                        }
                    }
                    else
                    {
                        _menuItem.Url.Url = _localizationChild.Url;
                    }

                    _model.MenuItems.Add(_menuItem);
                }
            }


            //pobieranie alerta
            if (_localization.GetPropertyValue<bool>("LangAlertActive"))
            {
                _model.Alert = new HeaderAlertViewModel()
                {
                    Title = _localization.GetPropertyValue<string>("LangAlertTitle"),
                    ButtonTitle = _localization.GetPropertyValue<string>("LangAlertButtonTitle"),
                    ButtonUrl = _localization.GetPropertyValue<string>("LangAlertButtonURL"),
                    IsButtonNewTab = _localization.GetPropertyValue<bool>("LangAlertButtonNewTab"),
                };
            }

            #region PobieranieOverlaya

            var overlayEnabledType = _localization.GetPropertyValue<int>("OverlayShowOn");
            var overlayIsEnabled = false;
            
            switch (library.GetPreValueAsString(overlayEnabledType))
            {
                case "Strona główna": // only main page
                    if (_currentPage.Id == _localizationNode.Id)
                        overlayIsEnabled = true;
                    break;
                case "Cała strona": // whole page
                    overlayIsEnabled = true;
                    break;
                case "Wybrana kategoria": // selected categories
                    overlayIsEnabled = _localization.GetPropertyValue<string>("OverlayShowOnNodes")
                        .Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                        .Any(id => _umbracoHelper.TypedContent(id).IsAncestorOrSelf(_currentPage));
                    break;
                case "Wyłączony": //off
                default:
                    break;
            }
            if (overlayIsEnabled)
            {
                _model.Overlay = new OverlayViewModel()
                {
                    DesktopImageUrl = _localization.GetPropertyValue<string>("OverlayPictureDesktop"),
                    MobileImageUrl = _localization.GetPropertyValue<string>("OverlayPictureMobile"),
                    ImageAlt = _localization.GetPropertyValue<string>("OverlayPictureAlt"),

                    ButtonUrl = _localization.GetPropertyValue<string>("OverlayButtonUrl"),
                    ButtonIsNewTab = _localization.GetPropertyValue<bool>("OverlayButtonNewTab"),

                    Capping = _localization.GetPropertyValue<int>("OverlayCapping"),
                };
            }

            #endregion

            return _model;
        }

        public FooterViewModel GetFooterViewModel(int currentUmbracoPageId)
        {
            var _model = new FooterViewModel();
            //Aktualna strona, na której się znajdujemy
            var _currentPage = _umbracoHelper.TypedContent(currentUmbracoPageId);
            //Aktualna lokalizacja, na której się znajdujemy
            var _localizationNode = _currentPage.AncestorOrSelf(DocumentTypeEnum.location.ToString());

            _model.Languages = _localizationNode.Parent.Children.Where("Visible").Select(q => new Link() { IsActive = true, Url = q.Url, Text = q.Name });
            _model.HomePageUrl = _localizationNode.Url;
            _model.MenuItems = new List<MenuItem>();

            foreach (var _localizationChild in _localizationNode.Children.Where("Visible"))
            {
                var _item = new SelectMenuOrFooter(_localizationChild);
                if (_item.ChoiceMenuOrFooter != null && _item.ChoiceMenuOrFooter.ToString() == MenuPositionTypeEnum.Footer.ToString())
                {
                    var _menuItem = new MenuItem();
                    _menuItem.Url = new Link() { Text = _localizationChild.Name, IsActive = true };

                    var _currentNodeChildren = _localizationChild.Children.Where("Visible");
                    _menuItem.SubMenuItems = new List<MenuItem>();
                    foreach (var item in _currentNodeChildren)
                    {
                        if (ItemShouldNotBeDisplayed(item))
                            continue;

                        MenuItem _footerItem;
                        if (item.DocumentTypeAlias == DocumentTypeEnum.searchConnection.ToString())
                        {
                            var _searchConnectionItem = _umbracoHelper.TypedContent(item.Id);
                            _footerItem = new MenuItem();
                            if (_searchConnectionItem != null)
                            {
                                foreach (var itemLink in _searchConnectionItem.GetPropertyValue<JArray>("link"))
                                {
                                    _footerItem = new MenuItem()
                                    {
                                        Url = new Link()
                                        {
                                            IsActive = true,
                                            Url = itemLink.Value<string>("link"),
                                            OpenInNewWindow = itemLink.Value<bool>("newWindow")
                                        }
                                    };
                                    _footerItem.Url.Text = _searchConnectionItem.Name;
                                }
                            }
                        }
                        else
                        {
                            _footerItem = new MenuItem()
                            {
                                Url = new Link()
                                {
                                    IsActive = true,
                                    Url = item.Url,
                                    Text = item.Name,
                                    DataCookie = item.IsDocumentType(AdvertisingOfSales.ModelTypeAlias) ? CookieVariables.AdvertisingFilterCookie : (item.IsDocumentType(ContractNotices.ModelTypeAlias) ? CookieVariables.ContractsFilterCookie : "")
                                }
                            };
                        }
                        _menuItem.SubMenuItems.Add(_footerItem);
                    }
                    _model.MenuItems.Add(_menuItem);
                }
            }

            if (_localizationNode.HasProperty("socialMediaElementList") && !string.IsNullOrEmpty(_localizationNode.GetPropertyValue<string>("socialMediaElementList")))
            {
                var _socialItems = _localizationNode.GetPropertyValue<ArchetypeModel>("socialMediaElementList");
                _model.SocialMediaItems = _socialItems.Fieldsets.Where(x => x != null && x.Properties.Any() && !x.Disabled).Select(q => new SocilaItem()
                {
                    SocialUrl = q.GetValue<string>("url"),
                    SocialName = q.GetValue<string>("type")
                });
            }

            var _localization = new Location(_localizationNode);
            if (_localization.AddBipurl != null)
            {
                foreach (var item in _localization.GetPropertyValue<JArray>("AddBipurl"))
                {
                    _model.BipUrl = item.Value<string>("link");
                    _model.BipUrlTarget = item.Value<bool>("newWindow");
                }
            }

            //Ustawianie id z sales manago 
            if (_accountService.IsAuthenticated())
            {
                var profile = new ProfileViewModel();
                profile = _accountService.GetProfileView(currentUmbracoPageId, profile);
                _model.SalesManagoContactId = profile.SalesmanagoContactId;
            }

            return _model;
        }

        private bool ItemShouldNotBeDisplayed(IPublishedContent content)
        {
            var isHidden = content.GetPropertyValue<bool>("isHidden");
            if (isHidden)
                return true;

            var onlyForGuests = new List<string> { "login", "register" };
            var onlyForAuthenticated = new List<string> { "userProfile", "logout" };
            var itemType = content.DocumentTypeAlias.ToString();

            return (onlyForGuests.Contains(itemType) && _accountService.IsAuthenticated())
                || (onlyForAuthenticated.Contains(itemType) && !_accountService.IsAuthenticated());
        }
    }
}
