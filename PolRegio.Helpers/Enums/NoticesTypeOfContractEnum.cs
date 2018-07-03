using nuPickers.Shared.EnumDataSource;

namespace PolRegio.Helpers.Enums
{
    /// <summary>
    /// Enum zawierający rodzaj zamówienia
    /// </summary>
    public enum NoticesTypeOfContractEnum
    {
        /// <summary>
        /// dostawa
        /// </summary>
        [EnumDataSource(Key = "1", Label = "dostawa")]
        Delivery = 1,
        /// <summary>
        /// usługa
        /// </summary>
        [EnumDataSource(Key = "2", Label = "usługa")]
        Service = 2,
        /// <summary>
        /// robota budowlana
        /// </summary>
        [EnumDataSource(Key = "3", Label = "robota budowlana")]
        BuildingWork = 3
    }
}
