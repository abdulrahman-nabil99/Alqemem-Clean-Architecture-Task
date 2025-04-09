using CleanArchTask.Application.Features.Employee.Commands.AddEditCmd;
using CleanArchTask.Application.Features.Employee.Commands.DeleteCmd;
using CleanArchTask.Application.Features.Employee.Queries.GetByIdQuery;
using CleanArchTask.Application.Features.Employee.Queries.GetEmployeesQuery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetEmployees")]
        public async Task<IActionResult> GetEmployees([FromQuery] GetEmployeesQuery request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] GetEmployeeByIdQuery request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromBody] AddEditEmployeeCmd request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] AddEditEmployeeCmd request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployees([FromBody] DeleteEmployeesCmd request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
