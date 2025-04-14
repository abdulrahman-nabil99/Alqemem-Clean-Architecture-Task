using CleanArchTask.Application.DTOs.Employee;
using CleanArchTask.Application.Features.Employee.Commands.AddEditCmd;
using CleanArchTask.Application.Features.Employee.Commands.DeleteCmd;
using CleanArchTask.Application.Features.Employee.Queries.GetEmployeesQuery;
using CleanArchTask.Domain.Common.Models;
using CleanArchTask.Domain.Entities;

namespace CleanArchTask.Application.Interfaces.Respositories
{
    public interface IEmployeeRepository
    {
        Task<(IEnumerable<EmployeesView>, int)> GetEmployeesAsync(GetEmployeesQuery request);
        Task<int> DeleteEmployeesAsync(DeleteEmployeesCmd cmd);
        Task<EmployeeDto> GetEmployeeByIdAsync(int id);
        Task<int> AddEmployeeAsync (AddEditEmployeeCmd request);
        Task<Response<int>> CheckEmailAndMobileExists(string email, string mobile, int? id);
        Task<bool> CheckEmailExists(string value, int? id);
        Task<bool> CheckMobileExists(string value, int? id);
        Task<int> UpdateEmployeeAsync (AddEditEmployeeCmd request);
    }
}
