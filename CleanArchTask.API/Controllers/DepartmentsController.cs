using CleanArchTask.Application.Features.Department.Query.GetDDLQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepartmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetDDL")]
        public async Task<IActionResult> GetDepartmentDDL()
        {
            var response = await _mediator.Send(new GetDepartmentDDLQuery());
            return StatusCode(response.StatusCode, response);
        }
    }
}
