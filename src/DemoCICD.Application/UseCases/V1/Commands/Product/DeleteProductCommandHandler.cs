using AutoMapper;
using DemoCICD.Contract.Abstractions.Message;
using DemoCICD.Contract.Services.V1.Product;
using DemoCICD.Contract.Shared;
using DemoCICD.Domain.Abstractions;
using DemoCICD.Domain.Abstractions.Repositories;
using static DemoCICD.Domain.Exceptions.ProductException;

namespace DemoCICD.Application.UseCases.V1.Commands.Product
{
    public sealed class DeleteProductCommandHandler : ICommandHandler<Command.DeleteProduct>
    {
        private readonly IRepositoryBase<Domain.Entities.Product, Guid> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteProductCommandHandler(IRepositoryBase<Domain.Entities.Product, Guid> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(Command.DeleteProduct request, CancellationToken cancellationToken)
        {
            var product = await _repository.FindByIdAsync(request.Id, cancellationToken , null);
            if (product == null)
            {
                throw new ProductNotFoundException(request.Id);
            }

            _repository.Remove(product);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }
    }
}
