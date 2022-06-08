using Application.Queries.Notifications;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Common;
using wms.Dto.Common.Responses;
using wms.Dto.Notifications;
using wms.Dto.Pagination;

namespace wms.Controllers.Api;

[Authorize]
public class NotificationsController : ApiControllerBase
{
    public NotificationsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }
    
    [HttpGet]
    public async Task<ActionResult<BaseResponse<PageViewModel<NotificationViewModel>>>> GetAll(
        [FromQuery] GetNotificationsQueryParams queryParams
        )
    {
        var query = Mapper.Map<GetAllNotificationsQuery>(queryParams);

        var notificationsPage = await Mediator.Send(query);

        return Ok(notificationsPage.ToViewModel<NotificationViewModel>(Mapper));
    }
}