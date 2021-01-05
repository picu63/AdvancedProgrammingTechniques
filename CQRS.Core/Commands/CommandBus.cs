using System.Threading;
using System.Threading.Tasks;
using CQRS.MediatR.Command;
using MediatR;

namespace CQRS.Core.Commands
{
    public class CommandBus : ICommandBus
    {
        private readonly IMediator _mediator;

        public CommandBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<Unit> Send(ICommand request, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}