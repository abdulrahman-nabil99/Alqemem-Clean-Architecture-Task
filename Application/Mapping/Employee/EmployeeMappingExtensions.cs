using CleanArchTask.Application.DTOs.Employee;
using CleanArchTask.Application.Features.Employee.Commands.AddEditCmd;
using MediatR;

namespace CleanArchTask.Application.Mapping.Employee
{
    public static class EmployeeMappingExtensions
    {
        public static EmployeeDto ToDto(this Domain.Entities.Employee employee)
        {
            return new EmployeeDto
            {
                Id = employee.Id,
                Age = employee.Age,
                FNameAr= employee.FNameAr,
                LNameAr= employee.LNameAr,
                FNameEn = employee.FNameEn,
                LNameEn = employee.LNameEn,
                Email = employee.Email,
                DepartmentId = employee.DepartmentId ?? 0,
                Address= employee.Address,
                MaritalStatusId= employee.MaritalStatusId ?? 0,
                Mobile = employee.Mobile,
            };
        }

        public static List<EmployeeDto> ToDtoList(this IEnumerable<Domain.Entities.Employee> employees)
        {
            return employees.Select(e => e.ToDto()).ToList();
        }

        public static Domain.Entities.Employee FromCommand(this AddEditEmployeeCmd cmd, Domain.Entities.Employee? copyTo = default)
        {
            var employee = copyTo ?? new Domain.Entities.Employee();
            employee.FNameAr = cmd.FNameAr;
            employee.FNameEn = cmd.FNameEn;
            employee.LNameAr = cmd.LNameAr;
            employee.LNameEn = cmd.LNameEn;
            employee.Email = cmd.Email;
            employee.DepartmentId = cmd.DepartmentId;
            employee.Age = cmd.Age;
            employee.Address = cmd.Address;
            employee.Mobile = cmd.Mobile;
            employee.MaritalStatusId = cmd.MaritalStatusId;

            return employee;
        }
    }
}
