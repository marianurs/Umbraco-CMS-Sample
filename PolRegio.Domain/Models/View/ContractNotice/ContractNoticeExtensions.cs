using System;
using PolRegio.Domain.Models.UmbracoCreate;
using PolRegio.Helpers.Enums;

namespace PolRegio.Domain.Models.View.ContractNoticeModel
{
    public static class ContractNoticeExtensions
    {
        public static NoticesSalesStatusEnum GetStatus(this ContractNotice instance)
        {
            return instance.DateLimitForReceipt <= DateTime.UtcNow.AddDays(-1)
                ? NoticesSalesStatusEnum.AfterOpen
                : NoticesSalesStatusEnum.BeforeOpen;
        }
        
    }
}