namespace CleanArchTask.Domain.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    }
}
