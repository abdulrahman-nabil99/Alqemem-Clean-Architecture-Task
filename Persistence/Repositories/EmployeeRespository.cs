using CleanArchTask.Application.Features.Employee.Commands.AddEditCmd;
using CleanArchTask.Application.Features.Employee.Queries.GetEmployeesQuery;
using CleanArchTask.Application.Interfaces.Respositories;
using CleanArchTask.Domain.Common.Enums;
using CleanArchTask.Domain.Entities;
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
            var employee = await _set.FindAsync(id);
            return employee;
        }
        public async Task<(IEnumerable<Employee>, int)> GetEmployeesAsync(GetEmployeesQuery request)
        {
            var query = _set.AsQueryable();
            int count = await _set.CountAsync();
            if (request.Mode == WorkingMode.Client)
            {
                var employees = await query.AsNoTracking().ToListAsync();
                return (employees, count);
            }else
            {
                // Sorting
                #region Sorting
                switch (request.SortColumn?.ToLower())
                {
                    case "fullnamear":
                        query = request.SortDirection.ToLower() == "desc"
                            ? query.OrderByDescending(x => x.FullNameAr)
                            : query.OrderBy(x => x.FullNameAr);
                        break;
                    case "fullnameen":
                        query = request.SortDirection.ToLower() == "desc"
                            ? query.OrderByDescending(x => x.FullNameEn)
                            : query.OrderBy(x => x.FullNameEn);
                        break;
                    case "departmentar":
                        query = request.SortDirection.ToLower() == "desc"
                            ? query.OrderByDescending(x => x.DepartmentAr)
                            : query.OrderBy(x => x.DepartmentAr);
                        break;
                    case "departmenten":
                        query = request.SortDirection.ToLower() == "desc"
                            ? query.OrderByDescending(x => x.DepartmentEn)
                            : query.OrderBy(x => x.DepartmentEn);
                        break;
                    case "id":
                    default:
                        query = request.SortDirection.ToLower() == "desc"
                            ? query.OrderByDescending(x => x.Id)
                            : query.OrderBy(x => x.Id);
                        break;
                }
                #endregion

                // Pagination
                var result = await query.Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize).AsNoTracking().ToListAsync();

                return (result, count);
            }

        }
        // -1 Not Found, -2 BadRequest
        public async Task<int> AddEmployeeAsync(AddEditEmployeeCmd request)
        {
            // Validations
            //
            if(true)
            {
                return -1 ;
            }
            Employee employee = new Employee() { };
            await _set.AddAsync(employee);
            var result = await _context.SaveChangesAsync();
            return result; // ?? or Employee Id
        }
        public async Task<int> UpdateEmployeeAsync(AddEditEmployeeCmd request)
        {
            // Validations
            //
            if (true)
            {
                return -2;
            }
            Employee employee = await _set.FindAsync(request.Id); // using Data 
            if (employee is null) return -1;
            _set.Update(employee);
            var result = await _context.SaveChangesAsync();
            return result;
        }
        // Return number of Affected Rows
        public async Task<int> DeleteEmployeesAsync(int[] ids)
        {
            var employees = await _set.Where(e => ids.Contains(e.Id))
                .ToListAsync();

            _context.Employees.RemoveRange(employees);
            var result = await _context.SaveChangesAsync();
            return result;
        }

    }
}
