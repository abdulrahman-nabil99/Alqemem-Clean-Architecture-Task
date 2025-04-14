using CleanArchTask.Domain.Common.Models;

namespace CleanArchTask.Application.Interfaces.Respositories
{
    public interface IMaritalStatusRepository
    {
        Task<IEnumerable<DDLItem<int>>> GetMaritalStatusDDL();
    }
}
