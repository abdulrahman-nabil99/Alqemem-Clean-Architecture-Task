using CleanArchTask.Domain.Common.Models;

namespace CleanArchTask.Application.Interfaces.Respositories
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<DDLItem<int>>> GetDepartmentDDL();
    }
}
