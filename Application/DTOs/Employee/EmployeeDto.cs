
namespace CleanArchTask.Application.DTOs.Employee
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FNameAr { get; set; }
        public string LNameAr { get; set; }
        public string FNameEn { get; set; }
        public string LNameEn { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public int? MaritalStatusId { get; set; }
        public int? DepartmentId { get; set; }
    }
}
