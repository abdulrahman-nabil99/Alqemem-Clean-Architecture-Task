using System.ComponentModel.DataAnnotations;

namespace CleanArchTask.Domain.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string FullNameAr {  get; set; }
        public string FullNameEn {  get; set; }
        public string DepartmentAr {  get; set; }
        public string DepartmentEn {  get; set; }
    }
}
