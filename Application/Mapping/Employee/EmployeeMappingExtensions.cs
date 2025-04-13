using CleanArchTask.Application.DTOs.Employee;

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
                FullNameAr= employee.FullNameAr,
                FullNameEn= employee.FullNameEn,
                DepartmentId = employee.DepartmentId ?? 0,
                DepartmentAr= employee.Department?.NameAr ?? "",
                DepartmentEn= employee.Department?.NameEn ?? "",
            };
        }

        public static List<EmployeeDto> ToDtoList(this IEnumerable<Domain.Entities.Employee> employees)
        {
            return employees.Select(e => e.ToDto()).ToList();
        }
    }
}
