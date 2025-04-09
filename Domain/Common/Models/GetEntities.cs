using CleanArchTask.Domain.Common.Enums;

namespace CleanArchTask.Domain.Common.Models
{
    public class GetEntities
    {
        public WorkingMode Mode { get; set; } = WorkingMode.Server;
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public string SortColumn { get; set; } = "";
        public string SortDirection { get; set; } = "";
    }
}
