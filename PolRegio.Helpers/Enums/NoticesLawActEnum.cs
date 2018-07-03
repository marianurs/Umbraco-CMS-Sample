using nuPickers.Shared.EnumDataSource;

namespace PolRegio.Helpers.Enums
{
    /// <summary>
    /// Enum zawierający akt prawny zamówienia
    /// </summary>
    public enum NoticesLawActEnum
    {
        /// <summary>
        /// ustawa Prawo zamówień publicznych
        /// </summary>
        [EnumDataSource(Key = "1", Label = "ustawa Prawo zamówień publicznych")]
        Law = 1,
        /// <summary>
        /// przepisy wewnętrzne Zamawiającego
        /// </summary>
        [EnumDataSource(Key = "2", Label = "przepisy wewnętrzne Zamawiającego")]
        InternalRules = 2
    }
}