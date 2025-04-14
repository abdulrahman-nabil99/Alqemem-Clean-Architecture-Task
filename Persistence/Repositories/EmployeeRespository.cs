using CleanArchTask.Application.DTOs.Employee;
using CleanArchTask.Application.Features.Employee.Commands.AddEditCmd;
using CleanArchTask.Application.Features.Employee.Commands.DeleteCmd;
using CleanArchTask.Application.Features.Employee.Queries.GetEmployeesQuery;
using CleanArchTask.Application.Interfaces.Respositories;
using CleanArchTask.Application.Mapping.Employee;
using CleanArchTask.Domain.Common.Enums;
using CleanArchTask.Domain.Common.Models;
using CleanArchTask.Domain.Entities;
using CleanArchTask.Persistence.Extenstions;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System.Globalization;
using System.Reflection;

namespace Persistence.Repositories
{
    public class EmployeeRespository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Employee> _set;
        private readonly DbSet<EmployeesView> _view;
        public EmployeeRespository(AppDbContext context)
        {
            _context = context;
            _set = _context.Set<Employee>();
            _view = _context.Set<EmployeesView>();
        }
        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            var employee = await _set.FirstOrDefaultAsync(e=>e.Id == id);
            return employee.ToDto();
        }
        public async Task<(IEnumerable<EmployeesView>, int)> GetEmployeesAsync(GetEmployeesQuery request)
        {
            var query = _view.AsQueryable();
            int count = await _view.CountAsync();
            if (request.Mode == WorkingMode.Client)
            {
                var employees = await query.AsNoTracking().ToListAsync();
                return (employees, count);
            }else
            {
                // Sorting
                query = query.ApplySorting(request.SortColumn,request.SortDirection);

                #region Sorting Using Expression
                //switch (request.SortColumn?.ToLower())
                //{
                //    case "fullnamear":
                //        query = query.ApplySortingUsingExp(e => e.FullNameAr, request.SortDirection);
                //        break;
                //    case "fullnameen":
                //        query = query.ApplySortingUsingExp(e => e.FullNameEn, request.SortDirection);
                //        break;
                //    case "departmentar":
                //        query = query.ApplySortingUsingExp(e => e.DepartmentAr ?? "", request.SortDirection);
                //        break;
                //    case "departmenten":
                //        query = query.ApplySortingUsingExp(e => e.DepartmentEn ?? "", request.SortDirection);
                //        break;
                //    case "email":
                //        query = query.ApplySortingUsingExp(e => e.Email ?? "", request.SortDirection);
                //        break;
                //    case "id":
                //    default:
                //        query = query.ApplySortingUsingExp(e => e.Id, request.SortDirection);
                //        break;
                //}
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
                FNameAr = request.FNameAr,
                FNameEn = request.FNameEn,
                LNameAr = request.LNameAr,
                LNameEn = request.LNameEn,
                Email = request.Email,
                DepartmentId = request.DepartmentId,
                Age = request.Age,
                MaritalStatusId = request.MaritalStatusId,
                Mobile = request.Mobile,
                Address = request.Address,
            };
            await _set.AddAsync(employee);
            var result = await _context.SaveChangesAsync();
            return result; // ?? or Employee Id
        }
        public async Task<int> UpdateEmployeeAsync(AddEditEmployeeCmd request)
        {
            Employee employee = await _set.FindAsync(request.Id); // using Data 
            employee.FNameAr = request.FNameAr;
            employee.FNameEn = request.FNameEn;
            employee.LNameAr = request.LNameAr;
            employee.LNameEn = request.LNameEn;
            employee.Email = request.Email;
            employee.DepartmentId = request.DepartmentId;
            employee.Age = request.Age;
            employee.Address = request.Address;
            employee.Mobile = request.Mobile;
            employee.MaritalStatusId = request.MaritalStatusId;

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

        public async Task<Response<int>> CheckEmailAndMobileExists(string email, string mobile, int? id)
        {
            bool exists = await _set.AnyAsync(e => (e.Email == email || e.Mobile == mobile) && e.Id != id);

            if (exists)
                return new Response<int>((int)ResponseStatusCode.BadRequest, "Mobile or Email is already in use");

            return null; // If neither exists
        }

        public async Task<bool> CheckEmailExists(string value, int? id)
        {
            bool exists = await _set.AnyAsync(e => e.Email == value && e.Id != id);
            return exists;
        }

        public async Task<bool> CheckMobileExists(string value, int? id)
        {
            bool exists = await _set.AnyAsync(e => e.Mobile == value && e.Id != id);
            return exists;
        }
    }
}
