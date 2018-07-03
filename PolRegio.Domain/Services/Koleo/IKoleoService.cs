using PolRegio.Domain.Models.View.SearchTicket;

namespace PolRegio.Domain.Services.Koleo
{
    public interface IKoleoService
    {
        void SetRedirectUrl(ref SearchTicketFormView model);

    }
}