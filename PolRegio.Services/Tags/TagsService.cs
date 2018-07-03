using PolRegio.Domain.Services.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolRegio.Domain.Models.View.Tags;
using Umbraco.Web;
using PolRegio.Helpers.Enums;
using Umbraco.Core.Models;
using PolRegio.Domain.Models.UmbracoCreate;
using PolRegio.Domain.Models.Components.Home;
using PolRegio.Domain.Models.Components.Tag;
using System.Globalization;

namespace PolRegio.Services.Tags
{
    public class TagsService : ITagsService
    {
        /// <summary>
        /// Obiekt typu UmbracoHelper
        /// </summary>
        private UmbracoHelper _umbracoHelper;
        public TagsService()
        {
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
        }

        public TagsViewModel SearchResult(int currentUmbracoPageId, string tagText)
        {
            var _model = new TagsViewModel();
            
            var current = _umbracoHelper.TypedContent(currentUmbracoPageId);
            _model.articleListWithTag = new ArticleListWithTag(current);
            var mainNode = current.AncestorOrSelf(2);
            var informationNode = mainNode.Descendant(DocumentTypeEnum.Information.ToString());
            
                var _newsNode = _umbracoHelper.TypedContent(informationNode.Id);
                var _newsList = _newsNode.Children.Where("Visible").Select(q => new ArticleWithDoubleFiltr(q));
                
                _model.NewsBoxesList = new List<TagItem>();
                foreach(var item in _newsList)
                {
                    if(item.ArticleTag != null && item.ArticleTag.ToString().Contains(tagText))
                    {
                        TagItem tagItem = new TagItem()
                        {
                            TagName = item.ArticleTitle,
                            Tagdesc = item.ListShortDescArticle.ToString(),
                            TagUrl = item.Url
                        };
                        _model.NewsBoxesList.Add(tagItem);
                    }
                }
            _model.TagText = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(tagText);
            return _model;
        }
        public Error404 errorPage(int currentUmbracoPageId)
        {
            var current = _umbracoHelper.TypedContent(currentUmbracoPageId);
            IPublishedContent _languageNode = current.AncestorOrSelf(DocumentTypeEnum.location.ToString());
            Error404 error = new Error404(_languageNode.Descendant(DocumentTypeEnum.error404.ToString()));
            return error;
        }
    }
}
