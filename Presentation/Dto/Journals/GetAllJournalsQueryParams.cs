using System.Text.Json.Serialization;
using Application.Common.Mappings;
using Application.Queries.Accounting;
using wms.Dto.Pagination;

namespace wms.Dto.Journals;

public class GetAllJournalsQueryParams : PaginationRequestParams, IMapFrom<GetJournalEntriesQuery>
{
    public int FromAccountId { get; set; }

    public int ToAccountId { get; set; }
}

public class GetAllCashDrawerJournalsQueryParams : PaginationRequestParams, IMapFrom<GetJournalEntriesQuery>
{
    public int ToAccountId { get; set; }
}