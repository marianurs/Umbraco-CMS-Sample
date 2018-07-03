namespace PolRegio.Domain.Models.View.Account
{
    public class AgreementViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Value { get; set; }
        public bool Required { get; set; }

        public bool Preference { get; set; }
    }
}
