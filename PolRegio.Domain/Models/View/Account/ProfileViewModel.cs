using System;
using PolRegio.Domain.Models.Components.Account;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using MultiSelectList = System.Web.Mvc.MultiSelectList;

namespace PolRegio.Domain.Models.View.Account
{
    public class ProfileViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserSurname { get; set; }

        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])[A-Za-z\d$@$!%*?&]{8,}")]
        public string UserPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("UserPassword")]
        public string UserConfirmPassword { get; set; }

        [RegularExpression(@"^[0-9\(\)\+\-]+$")]
        public string UserPhone { get; set; }

        [Required]
        [MinLength(1)]
        public string[] SelectedRegions { get; set; }
        public MultiSelectList RegionSelectList { get; set; }

        public List<AgreementViewModel> Agreements { get; set; }

        public int CurrentUmbracoPageId { get; set; }

        public CultureInfo CurrentPageCulture { get; set; }

        public ProfileResponse Response { get; set; }

        public string UserEmail { get; set; }

        public Guid? SalesmanagoContactId { get; set; }

    }
}
