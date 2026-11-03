using DemoCICD.Contract.Abstractions.Message;
using System.Drawing;

namespace DemoCICD.Contract.Services.Product
{
    public static class Command
    {
        public sealed record CreateProductCommand(string Name, decimal Price, string Description) : ICommand;

        public sealed record UpdateProductCommand(Guid Id, string Name, decimal Price, string Description) : ICommand;

        public sealed record DeleteProductCommand(Guid Id) : ICommand;
    }
}
