using CleanArchTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }
        public virtual DbSet<Employee> Employees { get; set; } 
    }
}
