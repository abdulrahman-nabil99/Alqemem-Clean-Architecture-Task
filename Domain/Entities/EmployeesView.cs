namespace CleanArchTask.Domain.Entities
{
    public class EmployeesView
    {
        public int Id { get; set; }
        public string FullNameAr { get; set; }
        public string FullNameEn { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string? MaritalStatusAr { get; set; }
        public string? MaritalStatusEn { get; set; }
        public string? DepartmentAr { get; set; }
        public string? DepartmentEn { get; set; }
    }
}
