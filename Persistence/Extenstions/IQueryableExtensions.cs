using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace CleanArchTask.Persistence.Extenstions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, string sortColumn = "", string sortDirection = "asc")
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                return query;

            var isDescending = sortDirection?.Trim().ToLower() == "desc";

            try
            {
                var reflectColumnName = typeof(T)
                    .GetProperty(sortColumn, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)?
                    .Name ?? string.Empty;
                if (string.IsNullOrEmpty(reflectColumnName))
                    throw new ArgumentException("Invalid Column Name is used");
                return isDescending
                    ? query.OrderByDescending(e => EF.Property<object>(e, reflectColumnName))
                    : query.OrderBy(e => EF.Property<object>(e, reflectColumnName));
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Invalid column name '{sortColumn}'", ex);
            }
        }

        public static IQueryable<T> ApplySortingUsingExp<T>(this IQueryable<T> query, Expression<Func<T,object>>? exp = null, string? sortDirection = "asc")
        {
            if (exp is null)
                return query;

            var isDescending = sortDirection?.Trim().ToLower() == "desc";

            try
            {
                return isDescending
                    ? query.OrderByDescending(exp)
                    : query.OrderBy(exp);
            }
            catch
            {
                throw;
            }
        }

        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            return query.Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize);
        }
    }
}
