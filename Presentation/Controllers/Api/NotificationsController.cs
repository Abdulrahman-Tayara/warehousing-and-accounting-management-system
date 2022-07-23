using Application.Common.Models;
using Application.Queries.Notifications;
using Application.Queries.Products;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Common.Responses;
using wms.Dto.Notifications;
using wms.Dto.Pagination;
using wms.Dto.Products;

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

        var productsQuery = new GetAllProductsQuery();
        var products = (await Mediator.Send(productsQuery))
            .AsEnumerable()
            .Where(product => notificationsPage.Any(notification => notification.ObjectId == product.Id))
            .Select(productEntity => Mapper.Map<ProductViewModel>(productEntity));

        var notificationsViewModelPage = notificationsPage
            .Select(notification => new NotificationViewModel
            {
                Id = notification.Id,
                NotificationType = notification.NotificationType,
                Product = products.First(product => product.Id == notification.ObjectId),
                IsValid = notification.IsValid
            })
            .AsQueryable()
            .AsPaginatedQuery(notificationsPage.CurrentPage, notificationsPage.PageSize);

        return Ok(notificationsViewModelPage);
    }
}