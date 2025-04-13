using System.ComponentModel.DataAnnotations;

namespace CleanArchTask.Domain.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string FullNameAr {  get; set; }
        public string FullNameEn {  get; set; }
        public int Age { get; set; }
        public int? DepartmentId { get; set; }
        public virtual Department? Department { get; set; }
    }
}
