using PolRegio.Domain.Models.View.SearchTicket;
using PolRegio.Domain.Services.Config;
using PolRegio.Domain.Services.EPodroznik;

namespace PolRegio.Services.EPodroznik
{
    public class EPodroznikService : IEPodroznikService
    {
        private const string ConfigKeyForRedirectFormat = "EPodroznikRedirectFormat";
        private readonly IConfigService _configService;

        public EPodroznikService(IConfigService configService)
        {
            _configService = configService;
        }


        public void SetRedirectUrl(ref SearchTicketFormView model)
        {
            if (!_configService.Custom.ContainsKey(ConfigKeyForRedirectFormat))
                return;
            var format = _configService.Custom[ConfigKeyForRedirectFormat];
            model.EPodroznikRedirectUrl = string.Format(format, model.StartStation, model.EndStation, model.Time, model.Date);
        }
    }
}