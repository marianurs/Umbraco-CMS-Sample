using PolRegio.Domain.Services.EventBus;

namespace PolRegio.Domain.Services.Account.Events
{
    public class AccountModifiedEvent : IEvent
    {
        public int UserId { get; set; }
        public string UserOldEmail { get; set; }
    }
}