using Application.Common.Models;
using Application.Common.QueryFilters;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Common;

public interface IGetPaginatedQuery
{
    public int Page { get; set; }

    public int PageSize { get; set; }
}

public class GetPaginatedQuery<TEntity> : IRequest<IPaginatedEnumerable<TEntity>>, IGetPaginatedQuery
{
    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}

public abstract class PaginatedQueryHandler<TRequest, TEntity> : IRequestHandler<TRequest, IPaginatedEnumerable<TEntity>>
    where TEntity : class, IEntity
    where TRequest : GetPaginatedQuery<TEntity>
{
    public async Task<IPaginatedEnumerable<TEntity>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var query = await GetQuery(request, cancellationToken);

        query = ApplyFilters(query, request);
        
        return query.AsPaginatedQuery(request.Page, request.PageSize);
    }

    protected abstract Task<IQueryable<TEntity>> GetQuery(TRequest request, CancellationToken cancellationToken);

    protected IQueryable<TEntity> ApplyFilters(IQueryable<TEntity> query, TRequest request)
    {
        return query.WhereFilters(request);
    }
}