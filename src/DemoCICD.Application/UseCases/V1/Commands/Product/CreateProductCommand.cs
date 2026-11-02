using DemoCICD.Application.Abstractions.Message;

namespace DemoCICD.Application.UseCases.V1.Commands.Product
{
    public sealed class CreateProductCommand : ICommand
    {
        public string Name { get; set; }
    }
}
