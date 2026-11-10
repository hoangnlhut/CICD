using Asp.Versioning;
using DemoCICD.Contract.Shared;
using DemoCICD.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoCICD.Presentation.Controllers.V2
{

    [ApiVersion(2)]
    public class CustomersController : ApiController
    {
        public CustomersController(ISender sender) : base(sender)
        {
        }

        [HttpGet("Customers")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Customers()
        {
            return Ok("Customer ok");
        }
    }
}
