namespace CleanArchTask.Domain.Common.Models
{
    public class DDLItem<T>
    {
        public int Id { get; set; }
        public string LabelAr { get; set; }
        public string LabelEn { get; set; }
        public T Value { get; set; }

    }
}
