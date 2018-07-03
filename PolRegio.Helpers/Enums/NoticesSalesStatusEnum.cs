using nuPickers.Shared.EnumDataSource;

namespace PolRegio.Helpers.Enums
{
    /// <summary>
    /// Enum zawierający status zamówienia lub sprzedaży
    /// </summary>
    public enum NoticesSalesStatusEnum
    {
        /// <summary>
        /// przed otwarciem ofert / wniosków
        /// </summary>
        [EnumDataSource(Key = "1", Label = "przed otwarciem ofert / wniosków")]
        BeforeOpen = 1,
        /// <summary>
        /// po otwarciu ofert / wniosków
        /// </summary>
        [EnumDataSource(Key = "2", Label = "po otwarciu ofert / wniosków")]
        AfterOpen = 2
    }
}