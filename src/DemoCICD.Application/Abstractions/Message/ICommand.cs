using DemoCICD.Contract.Shared;
using MediatR;

namespace DemoCICD.Application.Abstractions.Message
{
    public interface ICommand : IRequest<Result>
    {
    }

    public interface ICommand<TResponse> : IRequest<ResultT<TResponse>>
    {
    }
}
