namespace PolRegio.Domain.Services.EventBus
{
    public interface IEventHandler
    {
    }

    public interface IEventHandler<in TEvent> : IEventHandler where TEvent : IEvent
    {
        void Handle(TEvent @event);
    }
}