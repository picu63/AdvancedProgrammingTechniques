using System.Threading;
using System.Threading.Tasks;

namespace CQRS.MediatR.Event
{
    public interface IEventBus
    {
        Task Publish(IEvent @event, CancellationToken cancellationToken = default);
    }
}