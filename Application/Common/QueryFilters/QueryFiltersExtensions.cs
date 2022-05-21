namespace Application.Common.QueryFilters;

public static class QueryFiltersExtensions
{
    public static IQueryable<T> WhereFilters<T>(this IQueryable<T> queryable, object filter)
    {
        var adapter = new FilterConditionAdapter<T>(filter);

        var whereExpression = adapter.Build();

        if (whereExpression is not null)
            queryable = queryable.Where(whereExpression);

        return queryable;
    }
}