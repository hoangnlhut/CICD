using Asp.Versioning;
using DemoCICD.Contract.Shared;
using DemoCICD.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCICD.Presentation.Controllers.V3
{
    [ApiVersion(3)]
    public class OrdersController : ApiController
    {
        public OrdersController(ISender sender) : base(sender)
        {
        }

        [HttpGet("Orders")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Orders()
        {
            return Ok("Orders ok");
        }
    }
}
