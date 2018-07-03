namespace PolRegio.Domain.Services.EventBus
{
    public interface IEventBus
    {
        void Send<T>(T @event) where T : IEvent;
    }
}