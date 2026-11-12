using AutoMapper;
using DemoCICD.Contract.Abstractions.Message;
using DemoCICD.Contract.Services.V1.Product;
using DemoCICD.Contract.Shared;
using DemoCICD.Domain.Abstractions;
using DemoCICD.Domain.Abstractions.Repositories;
using DemoCICD.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoCICD.Application.UseCases.V1.Commands.Product
{
    public sealed class CreateProductCommandHandler : ICommandHandler<Command.CreateProduct>
    {
        private readonly IRepositoryBase<Domain.Entities.Product, Guid> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IRepositoryBase<Domain.Entities.Product, Guid> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(Command.CreateProduct request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.Product>(request);

            _repository.Add(entity);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }
    }
}
