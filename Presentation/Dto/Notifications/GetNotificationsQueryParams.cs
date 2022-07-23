using Application.Common.Mappings;
using Application.Queries.Notifications;
using Domain.Entities;
using wms.Dto.Pagination;

namespace wms.Dto.Notifications;

public class GetNotificationsQueryParams : PaginationRequestParams, IMapFrom<GetAllNotificationsQuery>
{
    public IEnumerable<int> ObjectIds { get; set; } = new List<int>();

    public NotificationType? NotificationType { get; set; } = default;
    
    public bool? ValidOnly { get; set; }
}