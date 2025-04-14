using CleanArchTask.Application.DTOs.Employee;
using CleanArchTask.Application.Interfaces.Respositories;
using CleanArchTask.Application.Mapping.Employee;
using CleanArchTask.Domain.Common.Enums;
using CleanArchTask.Domain.Common.Models;
using CleanArchTask.Domain.Entities;
using MediatR;

namespace CleanArchTask.Application.Features.Employee.Queries.GetEmployeesQuery
{
    public class GetEmployeesQuery: GetEntities,IRequest<Response<IEnumerable<EmployeesView>>>
    {

    }
    public class GetEmployeesHandler : IRequestHandler<GetEmployeesQuery, Response<IEnumerable<EmployeesView>>>
    {
        private readonly IEmployeeRepository _repo;

        public GetEmployeesHandler(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public async Task<Response<IEnumerable<EmployeesView>>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var (employees, count) = await _repo.GetEmployeesAsync(request);
                return new Response<IEnumerable<EmployeesView>>((int)ResponseStatusCode.OK, true, employees, count, "Data Retrieved");
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<EmployeesView>>((int) ResponseStatusCode.InternalServerError, ex.Message);

            }

        }
    }
}
