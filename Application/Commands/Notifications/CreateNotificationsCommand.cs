using Application.Common.Dtos;
using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Notifications;

public class CreateNotificationsCommand : IRequest<IEnumerable<int>>
{
    public IEnumerable<NotificationDto> NotificationDtos { get; }

    public CreateNotificationsCommand(IEnumerable<NotificationDto> notificationDtos)
    {
        NotificationDtos = notificationDtos;
    }
}

public class CreateNotificationsCommandHandler : IRequestHandler<CreateNotificationsCommand, IEnumerable<int>>
{
    private readonly INotificationRepository _notificationRepository;

    public CreateNotificationsCommandHandler(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    public async Task<IEnumerable<int>> Handle(CreateNotificationsCommand request, CancellationToken cancellationToken)
    {
        var saveNotificationsAction = await _notificationRepository.CreateAllAsync(
            request.NotificationDtos.Select(dto => new Notification
                {
                    ObjectId = dto.ObjectId,
                    NotificationType = dto.NotificationType
                }
            )
        );

        var notifications = await saveNotificationsAction();

        return notifications.Select(notification => notification.Id);
    }
}