using nuPickers.Shared.EnumDataSource;

namespace PolRegio.Helpers.Enums
{
    /// <summary>
    /// Enum zawierający rodzaj strony
    /// </summary>
    public enum OffersPageTypeEnum
    {
        [EnumDataSource(Key = "1", Label = "oferty krajowe")]
        national = 1,
        [EnumDataSource(Key = "2", Label = "oferty regionalne")]
        regional = 2,
        [EnumDataSource(Key = "3", Label = "oferty miedzynarodowe")]
        international = 3,
        [EnumDataSource(Key = "4", Label = "promocje")]
        sale = 4

    }
}