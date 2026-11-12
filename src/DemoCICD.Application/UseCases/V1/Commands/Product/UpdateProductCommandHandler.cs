using AutoMapper;
using DemoCICD.Contract.Abstractions.Message;
using DemoCICD.Contract.Services.V1.Product;
using DemoCICD.Contract.Shared;
using DemoCICD.Domain.Abstractions;
using DemoCICD.Domain.Abstractions.Repositories;
using static DemoCICD.Domain.Exceptions.ProductException;

namespace DemoCICD.Application.UseCases.V1.Commands.Product
{
    public sealed class UpdateProductCommandHandler : ICommandHandler<Command.UpdateProduct>
    {
        private readonly IRepositoryBase<Domain.Entities.Product, Guid> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IRepositoryBase<Domain.Entities.Product, Guid> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(Command.UpdateProduct request, CancellationToken cancellationToken)
        {
            var product = await _repository.FindByIdAsync(request.Id, cancellationToken, null);
            if (product == null)
            {
                throw new ProductNotFoundException(request.Id);
            }

            var entity = _mapper.Map<Domain.Entities.Product>(request);

            _repository.Update(entity);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }
    }
}
