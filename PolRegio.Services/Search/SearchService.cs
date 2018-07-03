using PolRegio.Domain.Services.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolRegio.Domain.Models.View.Search;
using Umbraco.Web;
using PolRegio.Helpers.Enums;
using PolRegio.Domain.Models.Components.Search;
using System.Threading;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Examine;
using Examine.SearchCriteria;

namespace PolRegio.Services.Search
{
    public class SearchService : ISearchService
    {
        /// <summary>
        /// Obiekt typu UmbracoHelper
        /// </summary>
        private UmbracoHelper _umbracoHelper;
        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        public SearchService()
        {
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
        }

        public SearchViewModel GetSearchViewModel(int currentUmbracoPageId)
        {
            var _model = new SearchViewModel();

            //Aktualna strona, na której się znajdujemy
            var _currentPage = _umbracoHelper.TypedContent(currentUmbracoPageId);

            //Aktualna lokalizacja, na której się znajdujemy

            var _localizationNode = _currentPage.AncestorOrSelf(DocumentTypeEnum.search.ToString());
            _model.CurrentPageCulture = Thread.CurrentThread.CurrentCulture;
            _model.CurrentUmbracoPageId = currentUmbracoPageId;
            _model.DisplayCount = new PolRegio.Domain.Models.UmbracoCreate.Search(_currentPage).DisplayItemCount;
            return SearchResult(_model);
        }
        public SearchViewModel SearchResult(SearchViewModel _searchModel)
        {
            var _currentPage = _umbracoHelper.TypedContent(_searchModel.CurrentUmbracoPageId);
            var _localizationNode = _currentPage.AncestorOrSelf(DocumentTypeEnum.location.ToString());
            if (!String.IsNullOrEmpty(_searchModel.SearchText))
            {
                var Searcher = Examine.ExamineManager.Instance.SearchProviderCollection["ExternalSearcher"];
                var searchCriteria = Searcher.CreateSearchCriteria(BooleanOperation.And);
                var query = searchCriteria.RawQuery(_searchModel.SearchText);
                _searchModel.SearchItems = new List<SearchItem>();
               
                //foreach (var item in ExamineManager.Instance.Search(_searchModel.SearchText, true))
                foreach (var item in Searcher.Search(query))
                {
                    IPublishedContent node = _umbracoHelper.TypedContent(item.Id);
                    if (node != null)
                    {
                        if (_localizationNode.IsAncestor(_umbracoHelper.TypedContent(node.Id)))
                        {
                            SearchItem _searchItem = new SearchItem() { SearchTitle = node.Name, SearchUrl = node.Url, Date = node.CreateDate };

                            if (item.Fields.ContainsKey(SearchFieldName(node.DocumentTypeAlias)))
                            {
                                _searchItem.SearchBody = SearchDesc(item.Fields[SearchFieldName(node.DocumentTypeAlias)]);
                            }
                            else if (item.Fields.ContainsKey("metaDescription"))
                            {
                                _searchItem.SearchBody = SearchDesc(item.Fields["metaDescription"]);
                            }
                            _searchModel.SearchItems.Add(_searchItem);
                        }
                    }
                }
                _searchModel.AllNewsCount = _searchModel.SearchItems.Count();
                _searchModel.SearchItems = _searchModel.SearchItems.Take(_searchModel.DisplayCount).ToList();
            }
            return _searchModel;
        }
        
        public SearchViewModel GetMoreSearchResult(string name, int skipCount, int displayCount, int currentPageId)
        {
            var _currentPage = _umbracoHelper.TypedContent(currentPageId);
            var _localizationNode = _currentPage.AncestorOrSelf(DocumentTypeEnum.location.ToString());
            SearchViewModel _searchModel = new SearchViewModel();
            if (!String.IsNullOrEmpty(name))
            {
                var Searcher = Examine.ExamineManager.Instance.SearchProviderCollection["ExternalSearcher"];
                var searchCriteria = Searcher.CreateSearchCriteria(BooleanOperation.And);
                var query = searchCriteria.RawQuery(name);
               
                _searchModel.SearchItems = new List<SearchItem>();
                foreach (var item in Searcher.Search(query))
                {
                    IPublishedContent node = _umbracoHelper.TypedContent(item.Id);
                    if (node != null)
                    {
                        if (_localizationNode.IsAncestor(_umbracoHelper.TypedContent(node.Id)))
                        {
                            SearchItem _searchItem = new SearchItem() { SearchTitle = node.Name, SearchUrl = node.Url, Date = node.CreateDate };

                            if (item.Fields.ContainsKey(SearchFieldName(node.DocumentTypeAlias)))
                            {
                                _searchItem.SearchBody = SearchDesc(item.Fields[SearchFieldName(node.DocumentTypeAlias)]);
                            }
                            else if (item.Fields.ContainsKey("metaDescription"))
                            {
                                _searchItem.SearchBody = SearchDesc(item.Fields["metaDescription"]);
                            }
                            _searchModel.SearchItems.Add(_searchItem);
                        }
                    }
                }

                _searchModel.SearchItems = _searchModel.SearchItems.Skip(skipCount).Take(displayCount).ToList();
            }
            return _searchModel;
        }

        public string SearchFieldName(string docTypeAlias)
        {
            switch (docTypeAlias)
            {
                case "article":
                    return "articleText";
                case "articleWithDoubleFiltr":
                    return "articleText";
                case "articleWithFilter":
                    return "articleText";
                case "disabilitiesSupport":
                    return "articleText";
                case "articleWithoutBox":
                    return "articleText";
                case "contractNotice":
                    return "contractText";
                case "announcementOfSale":
                    return "announcementText";
                default: return "";
            }
        }
        public string SearchDesc(string desc)
        {
            if (desc.Length > 150)
            {
                return desc.Substring(0, 150) + "...";
            }
            else
            {
                return desc;
            }
        }
    }
}