using System.Threading;
using System.Threading.Tasks;
using CQRS.MediatR.Event;
using MediatR;

namespace CQRS.Core.Events
{
    public class EventBus : IEventsBus
    {
        private readonly IMediator _mediator;

        public EventBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Publish(IEvent @event, CancellationToken cancellationToken = default)
        {
            await _mediator.Publish(@event, cancellationToken);
        }
    }
}