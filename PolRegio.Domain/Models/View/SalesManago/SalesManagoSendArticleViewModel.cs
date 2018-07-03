using System;
using System.Collections.Generic;

namespace PolRegio.Domain.Models.View.SalesManago
{
    public class SalesManagoSendArticleViewModel
    {
        public SalesManagoSendArticleViewModel()
        {
            Regions = new List<string>();
        }

        public List<string> Regions { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleLead { get; set; }
        public string ArticleLink { get; set; }
        public string Comment { get; set; }
        public string RedactorMail { get; set; }
        public string ImageUrl { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}