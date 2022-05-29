namespace Domain.Entities;

public class Notification : BaseEntity<int>
{
    public int ObjectId { get; set; }

    public NotificationType NotificationType { get; set; }

    public bool IsValid { get; set; } = true;
}

public enum NotificationType
{
    MinLevelExceeded
}