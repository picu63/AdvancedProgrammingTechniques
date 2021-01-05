using MediatR;

namespace CQRS.MediatR.Query
{
    public interface IQuery<out TQueryResult> : IRequest<TQueryResult>
    {
    }
}