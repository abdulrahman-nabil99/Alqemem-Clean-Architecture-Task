namespace CleanArchTask.Domain.Common.Models
{
    public class DeleteModel
    {
        public bool? isAllSelected { get; set; } = false;
        public int[] Ids { get; set; }
        public int[]? ExcludedIds { get; set; }
    }
}
