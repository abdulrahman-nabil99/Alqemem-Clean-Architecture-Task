using CleanArchTask.Application.DTOs.Employee;
using CleanArchTask.Application.Interfaces.Respositories;
using CleanArchTask.Domain.Common.Enums;
using CleanArchTask.Domain.Common.Models;
using CleanArchTask.Domain.Entities;
using MediatR;

namespace CleanArchTask.Application.Features.Employee.Queries.GetByIdQuery
{
    public class GetEmployeeByIdQuery: IRequest<Response<EmployeeDto>>
    {
        public int Id { get; set; }
    }

    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, Response<EmployeeDto>>
    {
        private readonly IEmployeeRepository _repo;

        public GetEmployeeByIdHandler(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public async Task<Response<EmployeeDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await _repo.GetEmployeeByIdAsync(request.Id);
                if (employee is null)
                    return new Response<EmployeeDto>((int)ResponseStatusCode.NotFound, false, null, null, "Employee Not Found");

                return new Response<EmployeeDto>((int)ResponseStatusCode.OK, true, employee, 1, "Data Retrieved");
            }
            catch (Exception ex)
            {
                return new Response<EmployeeDto>((int)ResponseStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}
