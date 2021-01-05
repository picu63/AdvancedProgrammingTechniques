using MediatR;

namespace CQRS.MediatR.Event
{
    public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
        where TEvent : IEvent
    {
    }
}