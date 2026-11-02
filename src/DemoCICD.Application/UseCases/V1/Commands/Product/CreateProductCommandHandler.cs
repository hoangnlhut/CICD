using DemoCICD.Contract.Shared;
using DemoCICD.Application.Abstractions.Message;

namespace DemoCICD.Application.UseCases.V1.Commands.Product
{
    public sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand>
    {
        public Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
