using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using PolRegio.Domain.Services.Account;
using PolRegio.Domain.Services.Config;
using PolRegio.Domain.Services.SalesManago;
using PolRegio.Helpers.Extensions;
using RestSharp;

namespace PolRegio.Services.SalesManago
{
    public class SalesManagoRecommendedArticleService : ISalesManagoRecommendedArticleService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountService _accountService;
        private readonly IConfigService _configService;

        public SalesManagoRecommendedArticleService(IUserRepository userRepository, IAccountService accountService,
            IConfigService configService)
        {
            _userRepository = userRepository;
            _accountService = accountService;
            _configService = configService;
        }

        public List<int> GetOffersIdForCurrentUser()
        {
            string offersFrameLink;
            var salesmanagoContactId = ArticleIdForCurrentUser();
            if (!salesmanagoContactId.HasValue
                || !_configService.Custom.TryGetValue("salesmanago_offers_frame", out offersFrameLink))
                return new List<int>();


            using (var webClient = new System.Net.WebClient())
            {
                var html = webClient.DownloadString(string.Format(offersFrameLink, salesmanagoContactId));
                var doc = new HtmlDocument();
                doc.LoadHtml(html);
                return doc.DocumentNode
                    .SelectSingleNode("//body")
                    .InnerText
                    .Trim()
                    .Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                    .MapToInt()
                    .ToList();
            }
        }


        private Guid? ArticleIdForCurrentUser()
        {
            string email = _accountService.CurrentUserEmail();
            return !_accountService.IsAuthenticated()
                ? null
                : _userRepository
                    .GetUserBy(db => db.UserEmail == email)
                    .SalesmanagoContactId;
        }
    }
}