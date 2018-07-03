using System.Collections.Generic;

namespace PolRegio.Domain.Models.Components.Account
{
    public class RegisterResponse
    {
        public bool ShouldDisplayMessage { get; set; }
        public bool IsError { get; set; }
        public string Message { get; set; }
        public List<KeyValuePair<string, string>> ValidationErrors { get; set; }
    }
}
