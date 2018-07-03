using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using PolRegio.Domain.Models.Events;
using PolRegio.Domain.Models.View.SalesManago;
using PolRegio.Domain.Services.EventBus;
using PolRegio.Domain.Services.Shared;
using PolRegio.Helpers.Web;
using umbraco.NodeFactory;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace PolRegio.Services.SalesManago
{
    public class SendEmailWithArticle : IEventHandler<UmbracoContentSavedEvent>
    {
        private const string ShouldNotifyPropName = "umbracoSalesManagoArticleNotifyShouldSend";
        private const string CommentPropName = "umbracoSalesManagoArticleNotifyComment";
        private const string EmailsPropName = "umbracoSalesManagoArticleNotifyNotificationEmails";

        private const string LeadPropName = "listShortDescArticle";
        private const string TitlePropName = "articleTitle";
        private const string ImagePropName = "listImage";

        private const string RegionsFilterPropName = "articleRegions";
        private const string RegionFilterPropName = "regionFiltr";

        private readonly IEmailService _emailService;


        public SendEmailWithArticle(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public void Handle(UmbracoContentSavedEvent @event)
        {
            var items = @event.Args.SavedEntities
                .Where(i => i.HasProperty(ShouldNotifyPropName)
                            && i.HasProperty(ShouldNotifyPropName)
                            && i.HasProperty(CommentPropName)
                            && i.HasProperty(LeadPropName)
                            && i.HasProperty(TitlePropName)) //Wybieramy artykuły z odpowiednimi polami do wysyłki 
                .Where(i => i.GetValue<bool>(ShouldNotifyPropName)); // i które są oznaczone do wysyłki


            foreach (var item in items)
            {
                //send mail
                var model = new SalesManagoSendArticleViewModel
                {
                    Comment = item.GetValue<string>(CommentPropName),
                    ArticleLead = item.GetValue<string>(LeadPropName),
                    ArticleTitle = item.GetValue<string>(TitlePropName),
                    ArticleLink = UmbracoContext.Current.UrlProvider.GetUrl(item.Id),
                    RedactorMail =
                        UmbracoContext.Current.Application.Services.UserService.GetUserById(item.WriterId).Email,
                    Regions = GetRegions(item),
                    ImageUrl = GetImage(item),
                    UpdateDate = item.UpdateDate
                };


                var emails = item.GetValue<string>(EmailsPropName)
                    .Split(',')
                    .Select(int.Parse)
                    .Select(i => new Node(i).Name);

                foreach (var email in emails)
                {
                    _emailService.SendArticleToSalesManago(model, email); 
                }


                //clear form data
                item.SetValue(ShouldNotifyPropName, false);
                item.SetValue(CommentPropName, "");
                item.SetValue(EmailsPropName, "");
            }
        }

        private static string GetImage(IContent item)
        {
            if (!item.HasProperty(ImagePropName)) return "";

            var umbHelper = new UmbracoHelper(UmbracoContext.Current);
            var content = umbHelper.Media(item.GetValue<string>(ImagePropName));
            return ((string) content.Url).ToAbsoluteUrl();
        }

        private List<string> GetRegions(IContent item)
        {
            if (item.HasProperty(RegionFilterPropName) || item.HasProperty(RegionsFilterPropName))
            {
                var propName = item.HasProperty(RegionFilterPropName)
                    ? RegionFilterPropName
                    : RegionsFilterPropName;

                return JsonConvert
                    .DeserializeObject<List<RegionFilterValue>>(item.GetValue<string>(propName))
                    .Select(r => r.Label)
                    .ToList();
            }

            return new List<string>() {"Ogólnopolskie"};
        }


        /// <summary>
        /// klasa do deserializacji regonów z 
        /// </summary>
        public class RegionFilterValue
        {
            public int Key { get; set; }
            public string Label { get; set; }
        }
    }
}