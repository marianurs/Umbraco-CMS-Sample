using System.Text.RegularExpressions;
using PolRegio.Domain.Models.View.SearchTicket;
using PolRegio.Domain.Services.Config;
using PolRegio.Domain.Services.Koleo;

namespace PolRegio.Services.Koleo
{
    public class KoleoService : IKoleoService
    {
        private const string ConfigKeyForRedirectFormat = "KoleoRedirectFormat";
        private readonly IConfigService _configService;
        private readonly string SEPARATOR = "-";

        public KoleoService(IConfigService configService)
        {
            _configService = configService;
        }


        public void SetRedirectUrl(ref SearchTicketFormView model)
        {
            if (!_configService.Custom.ContainsKey(ConfigKeyForRedirectFormat))
                return;
            var format = _configService.Custom[ConfigKeyForRedirectFormat];

            model.KoleoRedirectUrl = string.Format(format, Parametrize(model.StartStation),
                Parametrize(model.EndStation), model.Date, model.Time);
        }

        private string Parametrize(string str)
        {
            str = RemoveDiacritics(str);

            str = Regex.Replace(str, @"[_|\/\s]+", SEPARATOR);
            str = Regex.Replace(str, @"[\-]+", SEPARATOR);
            str = Regex.Replace(str, @"^-+|-+$", "");
            str = str.Replace(".", SEPARATOR);
            return str.ToLower();
        }

        private static string RemoveDiacritics(string text)
        {
            const string polishChars = "ąĄćĆęĘłŁńŃóÓśŚżŻźŹ";
            const string polishCharReplacements = "aAcCeElLnNoOsSzZzZ";

            for (var i = 0; i < polishChars.Length; i++)
            {
                text = text.Replace(polishChars[i], polishCharReplacements[i]);
            }
            return text;
        }
    }
}