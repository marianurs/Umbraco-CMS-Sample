using PolRegio.Domain.Services.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using PolRegio.Domain.Models.View.Article;
using Umbraco.Web;
using PolRegio.Domain.Models.Components;
using PolRegio.Domain.Services.Database;
using PolRegio.Domain.Models.Database;
using Newtonsoft.Json;
using PolRegio.Domain.Models.UmbracoCreate;
using PolRegio.Helpers.Enums;
using Umbraco.Core.Models;

namespace PolRegio.Services.Article
{
    /// <summary>
    /// Klasa implementująca interfejs IArticleService
    /// </summary>
    public class ArticleService : IArticleService
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
        public ArticleService(IDBService dbService)
        {
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
            _dbService = dbService;
        }

        public ArticlePageViewModel GetArticlePageViewModel(int currentUmbracoPageId)
        {
            var _currentArticle = _umbracoHelper.TypedContent(currentUmbracoPageId);

            var _model = new ArticlePageViewModel();
            _model.ArticleContent = new ArticleContent(_currentArticle);

            #region Pobranie Regionu i typu artykułu
            var _regionFilterItemsFromDB = _dbService.GetAll<RegionDB>("PolRegioRegion", q => q.IsEnabled);

            if (_currentArticle.DocumentTypeAlias == DocumentTypeEnum.articleWithFilter.ToString())
            {
                var _artilceWithFiltr = new ArticleWithFilter(_currentArticle);
                if (_artilceWithFiltr.RegionFiltr != null)
                {
                    var _articleRegion = JsonConvert.DeserializeObject<IEnumerable<NuPickersSqlDropDownPicker>>(_artilceWithFiltr.RegionFiltr.SavedValue.ToString()).FirstOrDefault();
                    if (_articleRegion != null)
                    {
                        var _regionFromDb = _regionFilterItemsFromDB.Where(q => q.Id.ToString() == _articleRegion.Key).FirstOrDefault();
                        if (_regionFromDb != null)
                        {
                            _model.ArticleRegion = _umbracoHelper.GetDictionaryValue(_regionFromDb.DictionaryKey);
                        }
                    }
                }
            }

            if (_currentArticle.DocumentTypeAlias == DocumentTypeEnum.articleWithDoubleFiltr.ToString())
            {
                var _artilceWithFiltr = new ArticleWithDoubleFiltr(_currentArticle);
                if (_artilceWithFiltr.ArticleCategory != null)
                {
                    var _articleType = JsonConvert.DeserializeObject<IEnumerable<NuPickersSqlDropDownPicker>>(_artilceWithFiltr.ArticleCategory.SavedValue.ToString()).FirstOrDefault();
                    _model.ArticleType = Enum.Parse(typeof(ArticleTypeEnum), _articleType.Key).ToString();
                }
                if (_artilceWithFiltr.ArticleRegions != null)
                {
                    var _articleRegion = JsonConvert.DeserializeObject<IEnumerable<NuPickersSqlDropDownPicker>>(_artilceWithFiltr.ArticleRegions.SavedValue.ToString()).FirstOrDefault();
                    if (_articleRegion != null)
                    {
                        var _regionFromDb = _regionFilterItemsFromDB.Where(q => q.Id.ToString() == _articleRegion.Key).FirstOrDefault();
                        if (_regionFromDb != null)
                        {
                            _model.ArticleRegion = _umbracoHelper.GetDictionaryValue(_regionFromDb.DictionaryKey);
                        }
                    }
                }
            }
            #endregion

            #region Galeria
            if (_model.ArticleContent.ArticleImageList != null)
            {
                _model.CarouselImages = _model.ArticleContent.ArticleImageList.Fieldsets.Where(x => x != null && x.Properties.Any() && !x.Disabled).Select(q => new ArticleCarouselItem()
                {
                    ArticleCarouselImage = q.GetValue<string>("addImage"),
                    ArticleCarouselDesc = q.GetValue<string>("imageCarouselDesc"),
                    ArticleCarouselName = q.GetValue<string>("imageName")
                });
            }
            #endregion

            #region Dokumenty
            if (_model.ArticleContent.AddDocumentDown != null)
            {
                _model.DownloadDocuments = _model.ArticleContent.AddDocumentDown.Fieldsets.Where(x => x != null && x.Properties.Any() && !x.Disabled).Select(q => new DownloadItem()
                {
                    DocumentUrl = q.GetValue<string>("addDoc"),
                    DocumentName = q.GetValue<string>("articleDocName"),
                    DocumentDate = q.GetValue<DateTime>("chooseDate")
                });
            }
            #endregion
            #region Tags
            if (_model.ArticleContent.ArticleTag != null)
            {
                var _localizationNode = _currentArticle.AncestorOrSelf(DocumentTypeEnum.location.ToString());
                var nodes = _localizationNode.Descendant(DocumentTypeEnum.articleListWithTag.ToString());

                IPublishedContent node = _umbracoHelper.TypedContent(nodes.Id);
                _model.TagListUrl = node.Url;


                var tagItem = _model.ArticleContent.ArticleTag.ToString().Split(',');
                _model.Tags = tagItem;
            }
            #endregion

            #region Data
            var _boxArticle = new ArticleBox(_currentArticle);
            if (_boxArticle != null && _boxArticle.ListArticleDate != DateTime.MinValue)
            {
                _model.ArticleDate = _boxArticle.ListArticleDate;
            }
            #endregion

            #region Sprawdzenie czy oferta czy informacja
            var _articleParent = _currentArticle.AncestorOrSelf(DocumentTypeEnum.OffersAndPromotions.ToString());
            if (_articleParent != null)
            {
                _model.IsOffersArticle = true;
            }
            #endregion
            return _model;
        }
    }
}
