using System.Collections;

namespace Application.Common.Models;

public interface IPaginatedEnumerable<out T> : IEnumerable<T>
{
    public int CurrentPage { get; set; }

    public int PageSize { get; set; }

    public int PagesCount { get; set; }

    public int RowsCount { get; set; }
}

public class PaginatedEnumerable<T> : IPaginatedEnumerable<T>
{
    private IEnumerable<T> data;
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int PagesCount { get; set; }

    public int RowsCount { get; set; }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return data.GetEnumerator();
    }

    public static PaginatedEnumerable<TModel> Create<TModel>(IQueryable<TModel> query, int page, int pageSize)
        where TModel : class
    {
        var rowsCount = query.Count();
        var pagesCount = (int) Math.Ceiling((double) rowsCount / pageSize);
        var skip = (page - 1) * pageSize;

        return new PaginatedEnumerable<TModel>
        {
            data = query.Skip(skip).Take(pageSize),
            CurrentPage = page,
            PagesCount = pagesCount,
            PageSize = pageSize,
            RowsCount = rowsCount
        };
    }

    public static PaginatedEnumerable<TModel> Create<TModel>(IQueryable<TModel> query)
        where TModel : class
    {
        return new PaginatedEnumerable<TModel>
        {
            data = query,
            CurrentPage = 1,
            PagesCount = 1,
            PageSize = query.Count(),
            RowsCount = query.Count(),
        };
    }
}

public static class QueryableExtensions
{
    public static IPaginatedEnumerable<T> AsPaginatedQuery<T>(this IQueryable<T> query, int page, int pageSize)
        where T : class
    {
        return PaginatedEnumerable<T>.Create(query, page, pageSize);
    }

    public static IPaginatedEnumerable<T> AsPaginatedQuery<T>(this IQueryable<T> query)
        where T : class
    {
        return PaginatedEnumerable<T>.Create(query);
    }
}