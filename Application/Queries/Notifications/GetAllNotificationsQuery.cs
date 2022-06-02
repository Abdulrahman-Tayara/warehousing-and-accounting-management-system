using Application.Queries.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Queries.Notifications;

public class GetAllNotificationsQuery : GetPaginatedQuery<Notification>
{
    public IEnumerable<int> ObjectIds { get; set; } = new List<int>();

    public NotificationType? NotificationType { get; set; } = default;

    public bool? IsValid { get; set; }
}

public class GetNotificationsQueryHandler : PaginatedQueryHandler<GetAllNotificationsQuery, Notification>
{
    private readonly INotificationRepository _notificationRepository;

    public GetNotificationsQueryHandler(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    protected override Task<IQueryable<Notification>> GetQuery(GetAllNotificationsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _notificationRepository
            .GetAll()
            .Where(notification => request.ObjectIds.Any(objId => notification.ObjectId == objId) ||
                                   !request.ObjectIds.Any())
            .Where(notification => notification.NotificationType == request.NotificationType ||
                                   request.NotificationType == default)
            .Where(notification => notification.IsValid == request.IsValid || !request.IsValid.GetValueOrDefault(true))
            .OrderByDescending(notification => notification.Id)
            .AsQueryable();

        return Task.FromResult(query);
    }
}