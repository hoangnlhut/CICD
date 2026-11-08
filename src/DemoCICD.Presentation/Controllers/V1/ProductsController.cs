using DemoCICD.Contract.Services.Product;
using DemoCICD.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoCICD.Presentation.Controllers.V1
{
    public class ProductsController : ApiController
    {
        public ProductsController(ISender sender) : base(sender)
        {
        }

        [HttpGet(Name = "GetProducts")]
        [ProducesResponseType(typeof(IEnumerable<Response.ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Products()
        {
            var query = new Query.GetProductQuery();
            var result = await Sender.Send(query);
            return Ok(result);
        }


    }
}
