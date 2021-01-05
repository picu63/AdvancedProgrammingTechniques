using MediatR;

namespace CQRS.MediatR.Query
{
    public interface IQueryHandler<in TQuery, TQueryResult> : IRequestHandler<TQuery, TQueryResult>
        where TQuery : IQuery<TQueryResult>
    {

    }
}