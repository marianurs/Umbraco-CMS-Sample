﻿using PolRegio.Domain.Models.Components.Account;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace PolRegio.Domain.Models.View.Account
{
    public class LoginFormViewModel
    {
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")]
        public string UserEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }

        public int CurrentUmbracoPageId { get; set; }

        public CultureInfo CurrentPageCulture { get; set; }

        public LoginResponse Response { get; set; }
    }
}
