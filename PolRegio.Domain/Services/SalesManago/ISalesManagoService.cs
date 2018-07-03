using PolRegio.Domain.Services.Account.Events;
using PolRegio.Domain.Services.EventBus;

namespace PolRegio.Domain.Services.SalesManago
{
    /// <summary>
    ///     Interfejs do obsługi komunikacji z SalesManago
    /// </summary>
    public interface ISalesManagoService : IEventHandler<NewAccountRegisteredEvent>,
        IEventHandler<AccountModifiedEvent>, IEventHandler<AccountDeletedEvent>
    {
    }
}