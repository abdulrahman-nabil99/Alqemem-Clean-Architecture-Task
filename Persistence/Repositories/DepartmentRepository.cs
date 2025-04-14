using CleanArchTask.Application.Interfaces.Respositories;
using CleanArchTask.Domain.Common.Models;
using CleanArchTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace CleanArchTask.Persistence.Repositories
{
    public class DepartmentRepository:IDepartmentRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Department> _set;
        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
            _set = _context.Set<Department>();
        }

        public async Task<IEnumerable<DDLItem<int>>> GetDepartmentDDL()
        {
            return await _set
                        .Select(item => new DDLItem<int>() { Id = item.Id, Value = item.Id, LabelAr =item.NameAr, LabelEn=item.NameEn })
                        .AsNoTracking().ToListAsync();
        }
    }
}
