using CleanArchTask.Application.Features.Employee.Commands.AddEditCmd;
using CleanArchTask.Application.Features.Employee.Commands.DeleteCmd;
using CleanArchTask.Application.Features.Employee.Queries.GetEmployeesQuery;
using CleanArchTask.Application.Interfaces.Respositories;
using CleanArchTask.Domain.Common.Enums;
using CleanArchTask.Domain.Entities;
using CleanArchTask.Persistence.Extenstions;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class EmployeeRespository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Employee> _set;
        public EmployeeRespository(AppDbContext context)
        {
            _context = context;
            _set = _context.Set<Employee>();
        }
        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            var employee = await _set.Include(e=>e.Department)
                .FirstOrDefaultAsync(e=>e.Id == id);
            return employee;
        }
        public async Task<(IEnumerable<Employee>, int)> GetEmployeesAsync(GetEmployeesQuery request)
        {
            var query = _set.Include(e=>e.Department).AsQueryable();
            int count = await _set.CountAsync();
            if (request.Mode == WorkingMode.Client)
            {
                var employees = await query.AsNoTracking().ToListAsync();
                return (employees, count);
            }else
            {
                // Sorting
                //query = query.ApplySorting(request.SortColumn,request.SortDirection);

                #region Sorting Using Expression
                switch (request.SortColumn?.ToLower())
                {
                    case "fullnamear":
                        query = query.ApplySortingUsingExp(e => e.FullNameAr, request.SortDirection);
                        break;
                    case "fullnameen":
                        query = query.ApplySortingUsingExp(e => e.FullNameEn, request.SortDirection);
                        break;
                    case "departmentar":
                        query = query.ApplySortingUsingExp(e => e.Department.NameEn ?? "", request.SortDirection);
                        break;
                    case "departmenten":
                        query = query.ApplySortingUsingExp(e => e.Department.NameEn ?? "", request.SortDirection);
                        break;
                    case "id":
                    default:
                        query = query.ApplySortingUsingExp(e => e.Id, request.SortDirection);
                        break;
                }
                #endregion

                // Pagination
                var result = await query
                    .ApplyPagination(request.PageNumber.Value,request.PageSize.Value)
                    .AsNoTracking().ToListAsync();

                return (result, count);
            }

        }
        // -1 Not Found, -2 BadRequest
        public async Task<int> AddEmployeeAsync(AddEditEmployeeCmd request)
        {
            Employee employee = new Employee() 
            {
                FullNameAr = request.FullNameAr,
                FullNameEn = request.FullNameEn,
                DepartmentId = request.DepartmentId,
                Age = request.Age,
            };
            await _set.AddAsync(employee);
            var result = await _context.SaveChangesAsync();
            return result; // ?? or Employee Id
        }
        public async Task<int> UpdateEmployeeAsync(AddEditEmployeeCmd request)
        {
            Employee employee = await _set.FindAsync(request.Id); // using Data 
            employee.FullNameEn = request.FullNameEn;
            employee.FullNameAr = request.FullNameAr;
            employee.DepartmentId = request.DepartmentId;
            employee.Age = request.Age;
            if (employee is null) return -1;
            _set.Update(employee);
            var result = await _context.SaveChangesAsync();
            return result;
        }
        // Return number of Affected Rows
        public async Task<int> DeleteEmployeesAsync(DeleteEmployeesCmd cmd)
        {
            if (cmd.isAllSelected == true)
            {
                var idsToExclude = cmd.ExcludedIds?.ToHashSet() ?? [];

                var employeesToDelete = await _set.Where(e => !idsToExclude.Contains(e.Id))
                    .ToListAsync();

                _context.Employees.RemoveRange(employeesToDelete);
            }
            else
            {
                var idsToDelete = cmd.Ids.ToHashSet();

                var employeesToDelete = await _set.Where(e => idsToDelete.Contains(e.Id))
                    .ToListAsync();

                _context.Employees.RemoveRange(employeesToDelete);
            }
            var result = await _context.SaveChangesAsync();
            return result;
        }

    }
}
