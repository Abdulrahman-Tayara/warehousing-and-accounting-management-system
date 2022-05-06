using Application.Queries.Payments;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using wms.Dto;
using wms.Dto.Common;
using wms.Dto.Common.Responses;
using wms.Dto.Pagination;

namespace wms.Controllers.Api;

public class PaymentsController : ApiControllerBase
{
    public PaymentsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponse<PaymentViewModel>>> Get(int id)
    {
        var payment = await Mediator.Send(new GetPaymentQuery {Id = id});

        return Ok(payment.ToViewModel<PaymentViewModel>(Mapper));
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<IEnumerable<PaymentViewModel>>>> GetAll(
        [FromQuery] PaginationRequestParams request)
    {
        var query = request.AsQuery<GetAllPaymentsQuery>();

        var payments = await Mediator.Send(query);

        return Ok(payments.ToViewModel<PaymentViewModel>(Mapper));
    }
}