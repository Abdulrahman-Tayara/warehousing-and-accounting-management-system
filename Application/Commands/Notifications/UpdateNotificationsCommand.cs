using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Notifications;

public class UpdateNotificationsCommand : IRequest<IEnumerable<int>>
{
    public IEnumerable<Notification> Notifications { get; set; }

    public UpdateNotificationsCommand(IEnumerable<Notification> notifications)
    {
        Notifications = notifications;
    }
}

public class UpdateNotificationCommandHandler : IRequestHandler<UpdateNotificationsCommand, IEnumerable<int>>
{
    private readonly INotificationRepository _notificationRepository;

    public UpdateNotificationCommandHandler(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    public async Task<IEnumerable<int>> Handle(UpdateNotificationsCommand request, CancellationToken cancellationToken)
    {

        var updatedNotifications = new List<Notification>();
        
        foreach (var notification in request.Notifications)
        {
            var updatedNotification = await _notificationRepository.Update(notification);
            updatedNotifications.Add(updatedNotification);
        }

        await _notificationRepository.SaveChanges();
        
        return updatedNotifications
            .Select(updatedNotification => updatedNotification.Id);
    }
}