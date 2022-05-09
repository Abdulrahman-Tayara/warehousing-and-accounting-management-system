using Application.Commands.Payments;
using Application.Queries.Payments;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using wms.Dto;
using wms.Dto.Common;
using wms.Dto.Common.Responses;
using wms.Dto.Pagination;
using wms.Dto.Payments;

namespace wms.Controllers.Api;

public class PaymentsController : ApiControllerBase
{
    public PaymentsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpPost("in/")]
    public async Task<ActionResult<BaseResponse<PaymentViewModel>>> CreateIn(CreatePaymentRequest request)
    {
        CreatePaymentCommand createPaymentCommand = Mapper.Map<CreatePaymentCommand>(request);
        createPaymentCommand.PaymentType = PaymentType.Normal;
        createPaymentCommand.PaymentIoType = PaymentIoType.In;

        var paymentId = await Mediator.Send(createPaymentCommand);

        var payment = await Mediator.Send(new GetPaymentQuery {Id = paymentId});

        return Ok(payment.ToViewModel<PaymentViewModel>(Mapper));
    }

    [HttpPost("out/")]
    public async Task<ActionResult<BaseResponse<PaymentViewModel>>> CreateOut(CreatePaymentRequest request)
    {
        CreatePaymentCommand createPaymentCommand = Mapper.Map<CreatePaymentCommand>(request);
        createPaymentCommand.PaymentType = PaymentType.Normal;
        createPaymentCommand.PaymentIoType = PaymentIoType.Out;

        var paymentId = await Mediator.Send(createPaymentCommand);

        var payment = await Mediator.Send(new GetPaymentQuery {Id = paymentId});

        return Ok(payment.ToViewModel<PaymentViewModel>(Mapper));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponse<PaymentViewModel>>> Get(int id)
    {
        var payment = await Mediator.Send(new GetPaymentQuery {Id = id});

        return Ok(payment.ToViewModel<PaymentViewModel>(Mapper));
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<IEnumerable<PaymentViewModel>>>> GetAll(
        [FromQuery] GetAllPaymentsRequest request
    )
    {
        var query = Mapper.Map<GetAllPaymentsQuery>(request);

        var payments = await Mediator.Send(query);

        return Ok(payments.ToViewModel<PaymentViewModel>(Mapper));
    }
}