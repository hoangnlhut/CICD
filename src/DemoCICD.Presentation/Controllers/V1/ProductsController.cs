using Asp.Versioning;
using Asp.Versioning;
using DemoCICD.Contract.Enumerations;
using DemoCICD.Contract.Extensions;
using DemoCICD.Contract.Services.V1.Product;
using DemoCICD.Contract.Shared;
using DemoCICD.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoCICD.Presentation.Controllers.V1
{
    [ApiVersion(1.0)]
    public class ProductsController : ApiController
    {
        public ProductsController(ISender sender) : base(sender)
        {
        }


        [HttpGet(Name = "GetProducts")]
        [ProducesResponseType(typeof(ResultT<PagedResult<Response.ProductResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Products(string? serchTerm = null,
        string? sortColumn = null,
        string? sortOrder = null,
        string? sortColumnAndOrder = null,
        int pageIndex = 1,
        int pageSize = 10)
        {
            var result = await Sender.Send(new Query.GetProductsQuery(serchTerm, sortColumn,
                SortOrderExtension.ConvertStringToSortOrder(sortOrder),
                SortOrderExtension.ConvertStringToSortOrderV2(sortColumnAndOrder),
                pageIndex,
                pageSize));
            return Ok(result);
        }

        [HttpGet("{productId}", Name = "GetProductById")]
        [ProducesResponseType(typeof(ResultT<Response.ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ProductById(Guid productId)
        {
            var query = new Query.GetProductByIdQuery(productId);
            var result = await Sender.Send(query);
            return Ok(result);
        }

        [HttpPost(Name = "CreateProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateProduct([FromBody] Command.CreateProduct request)
        {
            var result = await Sender.Send(request);
            return Ok(result);
        }

        [HttpPut("{productId}", Name = "UpdateProduct")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid productId, [FromBody] Command.UpdateProduct request)
        {
            var updateProductCommand = new Command.UpdateProduct(productId, request.Name, request.Price, request.Description);

            var result = await Sender.Send(updateProductCommand);
            return Ok(result);
        }

        [HttpDelete("{productId}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid productId)
        {
            var command = new Command.DeleteProduct(productId);
            var result = await Sender.Send(command);
            return Ok(result);
        }

    }
}
