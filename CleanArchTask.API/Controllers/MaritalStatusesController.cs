using CleanArchTask.Application.Features.MaritalStatus.Query.GetDDLQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaritalStatusesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MaritalStatusesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetDDL")]
        public async Task<IActionResult> GetMaritalStatusesDDL()
        {
            var response = await _mediator.Send(new GetMaritalStatusDDLQuery());
            return StatusCode(response.StatusCode, response);
        }
    }

}
