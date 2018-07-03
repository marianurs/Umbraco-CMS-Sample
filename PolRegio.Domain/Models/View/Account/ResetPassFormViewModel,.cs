using PolRegio.Domain.Models.Components.Account;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace PolRegio.Domain.Models.View.Account
{
    public class ResetPassFormViewModel
    {
        public string Token { get; set; }
        public bool TokenExpired { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])[A-Za-z\d$@$!%*?&]{8,}")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }

        public int CurrentUmbracoPageId { get; set; }

        public CultureInfo CurrentPageCulture { get; set; }

        public ResetPassResponse Response { get; set; }
    }
}
