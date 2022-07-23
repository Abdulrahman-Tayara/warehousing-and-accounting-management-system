using Application.Common.Mappings;
using Domain.Entities;
using wms.Dto.Common;
using wms.Dto.Products;

namespace wms.Dto.Notifications;

public class NotificationViewModel : IViewModel, IMapFrom<Notification>
{
    public int Id { get; set; }

    public ProductViewModel Product { get; set; }

    public NotificationType NotificationType { get; set; }

    public bool IsValid { get; set; }
}