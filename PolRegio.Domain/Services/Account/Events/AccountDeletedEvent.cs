using PolRegio.Domain.Services.EventBus;

namespace PolRegio.Domain.Services.Account.Events
{
    public class AccountDeletedEvent : IEvent
    {
        public int UserId { get; set; }
    }
}