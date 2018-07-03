using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using PolRegio.Cms.Models.SalesManago;
using PolRegio.Helpers.Extensions;
using PolRegio.Helpers.Web;
using umbraco;
using umbraco.NodeFactory;
using Umbraco.Web;
using Umbraco.Web.WebApi;

namespace PolRegio.Cms.Controllers
{
    //http://localhost:1200/Umbraco/Api/SalesManagoFeed/GetArticles

    public class SalesManagoFeedController : UmbracoApiController
    {
        private const string NewsAlias = "Aktualnosci";
        private const string OffersAlias = "Oferty";

        private readonly Dictionary<string, string> _regionsAliases = new Dictionary<string, string>
        {
            {"dolnoslaskie", "Dolnośląskie"},
            {"kujawskoPomorskie", "Kujawsko-Pomorskie"},
            {"lubelskie", "Lubelskie"},
            {"lubuskie", "Lubuskie"},
            {"lodzkie", "Łódzkie"},
            {"malopolskie", "Małopolskie"},
            {"mazowieckie", "Mazowieckie"},
            {"opolskie", "Opolskie"},
            {"podkarpackie", "Podkarpackie"},
            {"podlaskie", "Podlaskie"},
            {"pomorskie", "Pomorskie"},
            {"slaskie", "Sląskie"},
            {"swietokrzystkie", "świętokrzyskie"},
            {"warminskoMazurskie", "Warmińsko-Mazurskie"},
            {"wielkopolskie", "Wielkopolskie"},
            {"zachodniopomorskie", "Zachodniopomorskie"}
        };


        [HttpGet]
        public IHttpActionResult GetNews()
        {
            return GetArticles(NewsAlias);
        }

        [HttpGet]
        public IHttpActionResult GetOffers()
        {
            return GetArticles(OffersAlias);
        }

        private IHttpActionResult GetArticles(string docType)
        {
            var ret = new Dictionary<int, Article>();
            foreach (var langNode in uQuery.GetNodesByType("location"))
            {
                foreach (var region in _regionsAliases)
                {
                    var articles = langNode
                        .GetProperty<string>(region.Key + docType)
                        .MapToIdList()
                        .Select((id, index) => new {Id = id, Priority = index + 1});

                    foreach (var a in articles)
                    {
                        if (!ret.ContainsKey(a.Id))
                        {
                            var article = MapArticle(a.Id);
                            if (article == null)
                                continue;

                            ret.Add(a.Id, article);
                            ret[a.Id].Priority = a.Priority;
                        }

                        ret[a.Id].Regions.Add(region.Value);
                        ret[a.Id].Lang = langNode.Name; //article cannot have two lang nodes 
                        ret[a.Id].Priority = Math.Min(a.Priority, ret[a.Id].Priority); //quick workaround
                    }
                }
            }
            return Content(HttpStatusCode.OK, ret.Select(p => p.Value).ToList(), Configuration.Formatters.XmlFormatter);
        }

        private static Article MapArticle(int nodeId)
        {
            var umbHelper = new UmbracoHelper(UmbracoContext.Current);
            var article = new Node(nodeId);


            return !string.IsNullOrEmpty(article.Name) // unpublished articles returns null
                ? new Article
                {
                    Id = article.Id,
                    Url = article.Url,
                    FeaturedImageUrl = ((string) umbHelper
                            .Media(article.GetProperty<string>("listImage")).Url)
                        .ToAbsoluteUrl(),
                    Lead = article.GetProperty<string>("listShortDescArticle"),
                    Title = article.GetProperty<string>("listArticleTitle"),
                }
                : null;
        }
    }
}