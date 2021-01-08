using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace CQRS.MediatR.Command
{
    public interface ICommandBus
    {
        Task<Unit> Send(ICommand request, CancellationToken cancellationToken = default);
    }
}