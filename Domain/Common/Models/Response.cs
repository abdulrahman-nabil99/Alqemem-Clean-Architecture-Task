namespace CleanArchTask.Domain.Common.Models
{
    public class Response<T>
    {
        public int? RowCounts { get; set; }
        public T? Data { get; set; }
        public bool Success {  get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }

        public Response(int statusCode, bool success, T? data ,int? rowCounts, string? message)
        {
            RowCounts = rowCounts;
            StatusCode = statusCode;
            Success = success;
            Data = data;
            Message = message;
        }
        public Response(int statusCode, string? message) {
            RowCounts = null;
            StatusCode = statusCode;
            Success = false;
            Data = default;
            Message = message;
        }
    }
}
