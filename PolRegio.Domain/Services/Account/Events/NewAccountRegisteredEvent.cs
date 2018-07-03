using PolRegio.Domain.Services.EventBus;

namespace PolRegio.Domain.Services.Account.Events
{
    public class NewAccountRegisteredEvent : IEvent
    {
        public int UserId { get; set; }
    }
}