using System;
using PolRegio.Domain.Models.UmbracoCreate;
using PolRegio.Helpers.Enums;

namespace PolRegio.Domain.Models.View.AdvertisingOfSalesModel
{
    public static class AnnouncementOfSaleExtensions
    {
        public static NoticesSalesStatusEnum GetStatus(this AnnouncementOfSale instance)
        {
            return instance.DateLimitForReceipt <= DateTime.UtcNow.AddDays(-1)
                ? NoticesSalesStatusEnum.AfterOpen
                : NoticesSalesStatusEnum.BeforeOpen;
        }
    }
}