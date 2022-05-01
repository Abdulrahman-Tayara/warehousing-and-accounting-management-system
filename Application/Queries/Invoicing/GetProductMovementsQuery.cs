using Application.Queries.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Queries.Invoicing;

public class GetProductMovementsQuery : GetPaginatedQuery<ProductMovement>
{
    public int? InvoiceId { get; set; }
}

public class GetProductMovementsQueryHandler : PaginatedQueryHandler<GetProductMovementsQuery, ProductMovement>
{
    private readonly IProductMovementRepository _productMovementRepository;

    public GetProductMovementsQueryHandler(IProductMovementRepository productMovementRepository)
    {
        _productMovementRepository = productMovementRepository;
    }

    protected override Task<IQueryable<ProductMovement>> GetQuery(GetProductMovementsQuery request,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(
            _productMovementRepository.GetAll(new GetAllOptions<ProductMovement>
            {
                IncludeRelations = true,
                Filter = movement => request.InvoiceId == null || movement.InvoiceId.Equals(request.InvoiceId)
            }));
    }
}