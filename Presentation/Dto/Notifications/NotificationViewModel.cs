using Application.Common.Mappings;
using Domain.Entities;
using wms.Dto.Common;

namespace wms.Dto.Notifications;

public class NotificationViewModel : IViewModel, IMapFrom<Notification>
{
    public int ObjectId { get; set; }

    public NotificationType NotificationType { get; set; }

    public bool IsValid { get; set; }
}