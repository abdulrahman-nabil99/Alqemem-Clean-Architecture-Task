using CleanArchTask.Application.DTOs.Employee;
using CleanArchTask.Application.Interfaces.Respositories;
using CleanArchTask.Application.Mapping.Employee;
using CleanArchTask.Domain.Common.Enums;
using CleanArchTask.Domain.Common.Models;
using MediatR;

namespace CleanArchTask.Application.Features.Employee.Queries.GetEmployeesQuery
{
    public class GetEmployeesQuery: GetEntities,IRequest<Response<IEnumerable<EmployeeDto>>>
    {

    }
    public class GetEmployeesHandler : IRequestHandler<GetEmployeesQuery, Response<IEnumerable<EmployeeDto>>>
    {
        private readonly IEmployeeRepository _repo;

        public GetEmployeesHandler(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public async Task<Response<IEnumerable<EmployeeDto>>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var (employees, count) = await _repo.GetEmployeesAsync(request);
                var dtoList = employees.ToDtoList();
                return new Response<IEnumerable<EmployeeDto>>((int)ResponseStatusCode.OK, true, dtoList, count, "Data Retrieved");
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<EmployeeDto>>((int) ResponseStatusCode.InternalServerError, ex.Message);

            }

        }
    }
}
