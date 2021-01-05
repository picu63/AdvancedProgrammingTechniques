using MediatR;

namespace CQRS.MediatR.Command
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
        where TCommand : ICommand
    {
    }
}