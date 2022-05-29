using Domain.Entities;

namespace Application.Common.Dtos;

public class NotificationDto
{
    public int ObjectId { get; }

    public NotificationType NotificationType { get; }

    public NotificationDto(int objectId, NotificationType notificationType)
    {
        ObjectId = objectId;
        NotificationType = notificationType;
    }
}