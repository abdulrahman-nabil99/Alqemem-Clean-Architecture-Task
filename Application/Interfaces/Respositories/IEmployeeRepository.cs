using CleanArchTask.Application.Features.Employee.Commands.AddEditCmd;
using CleanArchTask.Application.Features.Employee.Commands.DeleteCmd;
using CleanArchTask.Application.Features.Employee.Queries.GetEmployeesQuery;
using CleanArchTask.Domain.Entities;

namespace CleanArchTask.Application.Interfaces.Respositories
{
    public interface IEmployeeRepository
    {
        Task<(IEnumerable<Employee>, int)> GetEmployeesAsync(GetEmployeesQuery request);
        Task<int> DeleteEmployeesAsync(DeleteEmployeesCmd cmd);
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<int> AddEmployeeAsync (AddEditEmployeeCmd request);
        Task<int> UpdateEmployeeAsync (AddEditEmployeeCmd request);
    }
}
