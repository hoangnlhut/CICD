using DemoCICD.Contract.Shared;
using MediatR;

namespace DemoCICD.Application.Abstractions.Message
{
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
        where TCommand : ICommand
    {
    }

    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, ResultT<TResponse>>
        where TCommand : ICommand<TResponse>
    {
    }
}
