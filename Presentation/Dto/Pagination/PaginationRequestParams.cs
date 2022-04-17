using Application.Queries.Common;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace wms.Dto.Pagination;

public class PaginationRequestParams
{
    [FromQuery(Name = "page")] public int Page { get; set; } = 1;

    [FromQuery(Name = "page_size")] public int PageSize { get; set; } = 10;
}

public static class PaginationParamsExtension
{
    public static T AsQuery<T>(this PaginationRequestParams requestParams)
        where T : IGetPaginatedQuery
    {
        return requestParams.AsQuery(Activator.CreateInstance<T>());
    }
    
    public static T AsQuery<T>(this PaginationRequestParams requestParams, T query)
        where T : IGetPaginatedQuery
    {
        query.Page = requestParams.Page;
        query.PageSize = requestParams.PageSize;

        return query;
    }
}