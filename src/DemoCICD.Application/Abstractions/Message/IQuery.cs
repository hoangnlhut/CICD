using DemoCICD.Contract.Shared;
using MediatR;

namespace DemoCICD.Application.Abstractions.Message
{
    public interface IQuery : IRequest<Result>
    {
    }

    public interface IQuery<TResponse> : IRequest<ResultT<TResponse>>
    {
    }
}
