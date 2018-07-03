using PolRegio.Domain.Services.BipModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolRegio.Domain.Models.View.BipPage;
using System.Threading;
using Umbraco.Web;
using PolRegio.Domain.Models.UmbracoCreate;
using umbraco.cms.businesslogic.web;
using PolRegio.Helpers.Enums;
using PolRegio.Domain.Models.Components.BIP;
using Umbraco.Core.Services;
using Umbraco.Core;
using System.Text.RegularExpressions;
using PolRegio.Domain.Models.Components.BipPage;
using Examine;
using Examine.SearchCriteria;

namespace PolRegio.Services.BipModels
{
    /// <summary>
    /// Klasa implementująca interfejs IBIPService
    /// </summary>
    public class BIPService : IBIPService
    {
        /// <summary>
        /// Obiekt typu UmbracoHelper
        /// </summary>
        private UmbracoHelper _umbracoHelper;
        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        public BIPService()
        {
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
        }
        /// <summary>
        /// Metoda zwracająca obiekt typu BIPFormViewModel zawierający 
        /// elementy wyświetlane na stronie formularza BIP
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns></returns>
        public BIPFormViewModel GetBipFormView(int currentUmbracoPageId)
        {
            var _model = new BIPFormViewModel();
            _model.CurrentUmbracoPageId = currentUmbracoPageId;
            _model.CurrentPageCulture = Thread.CurrentThread.CurrentCulture;
            return _model;
        }
        public BIPArticleViewModel BIPArticleViewModel(int currentUmbracoPageId)
        {
            var _model = new BIPArticleViewModel();

            //Aktualna strona, na której się znajdujemy
            var _currentPage = _umbracoHelper.TypedContent(currentUmbracoPageId);

            _model.Article = new ArticleBip(_currentPage);
            Document document = new Document(_currentPage.Id);
            DocumentVersionList[] versionList = document.GetVersions();

            // versionList = versionList.Where(x => x.Text != "").ToArray();
            //var contentService = ApplicationContext.Current.Services.ContentService;
            //var versionList = contentService.GetVersions(_currentPage.Id).ToArray();
            //var dsds = vvv.Where(x=>x.Status == Umbraco.Core.Models.ContentStatus.Published).ToArray();
            //var older = umbraco.cms.businesslogic.Content.GetContentFromVersion(versionList[0].Version);


            _model.ArticleChanges = new List<ArticleBipChanges>();
            for (int i = 0; i < versionList.Length; i++)
            {
                var versionContent = umbraco.cms.businesslogic.Content.GetContentFromVersion(versionList[i].Version);
                //var contentVersion = contentService.GetByVersion(versionList[i].Version);

                if (versionContent.getProperty("descriptionChanges").Value != null)
                {
                    ArticleBipChanges articleChange = new ArticleBipChanges()
                    {
                        ChangesDate = versionList[i].Date,
                        UserName = versionList[i].User.Name,
                        ChangeDescription = versionContent.getProperty("descriptionChanges").Value.ToString()
                    };
                    if (i == versionList.Length - 1)
                    {
                        articleChange.ChangeType = "Dodanie artykułu";
                    }
                    else
                    {
                        articleChange.ChangeType = "Edycja artykułu";
                    }

                    _model.ArticleChanges.Add(articleChange);
                }
            }
            return _model;

        }
        /// <summary>
        /// Metoda zwracająca klasę zawierającą elementy wyświetlane na stronie głównej BIP
        /// </summary>
        /// <param name="currentUmbracoPageId">id strony umbraco na której się znajdujemy</param>
        /// <returns>Model zawierający elementy strony głównej BIP</returns>
        public BIPPageViewModel BIPPageViewModel(int currentUmbracoPageId)
        {
            var _model = new BIPPageViewModel()
            {
                CurrentUmbracoPageId = currentUmbracoPageId,
                CurrentPageCulture = Thread.CurrentThread.CurrentCulture
            };

            return BIPPageViewModel(_model);
        }

        /// <summary>
        /// Metoda zwracająca klasę zawierającą elementy wyświetlane na stronie głównej BIP
        /// </summary>
        /// <param name="model">model BIPPageViewModel</param>
        /// <returns>Model zawierający elementy strony głównej BIP</returns>
        public BIPPageViewModel BIPPageViewModel(BIPPageViewModel model)
        {
            var _currentPage = _umbracoHelper.TypedContent(model.CurrentUmbracoPageId);
            var _bipPage = new BIP(_currentPage);

            if (!string.IsNullOrEmpty(model.SearchText))
            {
                var _searcher = ExamineManager.Instance.SearchProviderCollection["BipContentSearchSearcher"];
                var _searchCriteria = _searcher.CreateSearchCriteria(Examine.SearchCriteria.BooleanOperation.Or);
                var q_split = model.SearchText.Split(' ');
                var _fieldsToSearch = new[] { "pageTitle", "articleTitle", "articleText", "pageMainTitle", "pageMainDescription", "nodeName" };
                IBooleanOperation _filter = _searchCriteria.GroupedOr(_fieldsToSearch, q_split.First());
                foreach (var term in q_split.Skip(1))
                {
                    _filter = _filter.Or().GroupedOr(_fieldsToSearch, term);
                }
                var _searchResults = _searcher.Search(_filter.Compile()).OrderByDescending(x => x.Score);
                var _resultBipFormIds = _searchResults.Where(q => q.Fields["nodeTypeAlias"] == DocumentTypeEnum.bIPForm.ToString()).Select(q => q.Id);
                var _resultBipArticlesIds = _searchResults.Where(q => q.Fields["nodeTypeAlias"] == DocumentTypeEnum.articleBip.ToString()).Select(q => q.Id);

                model.ItemList.AddRange(_bipPage.Descendants(DocumentTypeEnum.bIPForm.ToString()).Where(q => _resultBipFormIds.Contains(q.Id)).Select(q => new BIpform(q)).Select(q => new BipListItemModel() { CreateDate = q.CreateDate, Url = q.Url, Description = removeHtmlTag(q.PageMainDescription.ToString()), Title = q.PageMainTitle }));
                model.ItemList.AddRange(_bipPage.Descendants(DocumentTypeEnum.articleBip.ToString()).Where(q => _resultBipArticlesIds.Contains(q.Id)).Select(q => new ArticleBip(q)).Select(q => new BipListItemModel() { CreateDate = q.CreateDate, Url = q.Url, Description = removeHtmlTag(q.ArticleText.ToString()), Title = q.ArticleTitle }));
            }
            else
            {
                var _bipForms = _bipPage.Descendants(DocumentTypeEnum.bIPForm.ToString()).Select(q => new BIpform(q));
                var _bipArticles = _bipPage.Descendants(DocumentTypeEnum.articleBip.ToString()).Select(q => new ArticleBip(q));

                model.ItemList.AddRange(_bipForms.Select(q => new BipListItemModel() { CreateDate = q.CreateDate, Url = q.Url, Description = removeHtmlTag(q.PageMainDescription.ToString()), Title = q.PageMainTitle }));
                model.ItemList.AddRange(_bipArticles.Select(q => new BipListItemModel() { CreateDate = q.CreateDate, Url = q.Url, Description = removeHtmlTag(q.ArticleText.ToString()), Title = q.ArticleTitle }));

            }

            return model;
        }

        public string removeHtmlTag(string text)
        {
            string textWithoutTags;
            int count;
            textWithoutTags = Regex.Replace(text, "<.*?>|&.*?;", string.Empty);
            if (textWithoutTags.Length > 160)
            {
                textWithoutTags = textWithoutTags.Substring(0, 160);
                count = textWithoutTags.LastIndexOf(' ');
                textWithoutTags = textWithoutTags.Substring(0, count) + "...";
            }
            else
            {
                textWithoutTags = textWithoutTags + "...";
            }

            return textWithoutTags;
        }
    }
}
