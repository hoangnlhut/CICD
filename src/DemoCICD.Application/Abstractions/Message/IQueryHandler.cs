using DemoCICD.Contract.Shared;
using MediatR;

namespace DemoCICD.Application.Abstractions.Message
{
    public interface IQueryHandler<TQuery> : IRequestHandler<TQuery, Result>
        where TQuery : IQuery
    {
    }

    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, ResultT<TResponse>>
        where TQuery : IQuery<TResponse>
    {
    }
}
