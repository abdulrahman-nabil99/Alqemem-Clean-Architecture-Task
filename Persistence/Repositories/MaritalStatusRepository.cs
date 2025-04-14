using CleanArchTask.Application.Interfaces.Respositories;
using CleanArchTask.Domain.Common.Models;
using CleanArchTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace CleanArchTask.Persistence.Repositories
{
    public class MaritalStatusRepository : IMaritalStatusRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<MaritalStatus> _set;
        public MaritalStatusRepository(AppDbContext context)
        {
            _context = context;
            _set = _context.Set<MaritalStatus>();
        }
        public async Task<IEnumerable<DDLItem<int>>> GetMaritalStatusDDL()
        {
            return await _set
            .Select(item => new DDLItem<int>() { Id = item.Id, Value = item.Id, LabelAr = item.NameAr, LabelEn = item.NameEn })
            .AsNoTracking().ToListAsync();
        }
    }
    
}
