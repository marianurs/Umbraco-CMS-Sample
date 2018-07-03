using PolRegio.Domain.Models.View.SearchTicket;

namespace PolRegio.Domain.Services.EPodroznik
{
    public interface IEPodroznikService
    {
        void SetRedirectUrl(ref SearchTicketFormView model);
    }
}