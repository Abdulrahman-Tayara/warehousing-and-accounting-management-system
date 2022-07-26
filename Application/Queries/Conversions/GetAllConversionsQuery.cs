using Application.Queries.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Queries.Conversions;

public class GetAllConversionsQuery : GetPaginatedQuery<Conversion>
{
}

public class GetAllConversionsQueryHandler : PaginatedQueryHandler<GetAllConversionsQuery, Conversion>
{
    private readonly IConversionRepository _conversionRepository;

    public GetAllConversionsQueryHandler(IConversionRepository conversionRepository)
    {
        _conversionRepository = conversionRepository;
    }

    protected override Task<IQueryable<Conversion>> GetQuery(GetAllConversionsQuery request,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(_conversionRepository.GetAll());
    }
}