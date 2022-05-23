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
    public Task<ActionResult<BaseResponse<PaymentViewModel>>> CreateIn(CreatePaymentRequest request)
    {
        return CreatePayment(request, PaymentType.Normal, PaymentIoType.In);
    }

    [HttpPost("out/")]
    public Task<ActionResult<BaseResponse<PaymentViewModel>>> CreateOut(CreatePaymentRequest request)
    {
        return CreatePayment(request, PaymentType.Normal, PaymentIoType.Out);
    }
    
    [HttpPost("in/discount/")]
    public Task<ActionResult<BaseResponse<PaymentViewModel>>> CreateDiscountIn(CreatePaymentRequest request)
    {
        return CreatePayment(request, PaymentType.Discount, PaymentIoType.In);
    }

    [HttpPost("out/discount/")]
    public Task<ActionResult<BaseResponse<PaymentViewModel>>> CreateDiscountOut(CreatePaymentRequest request)
    {
        return CreatePayment(request, PaymentType.Discount, PaymentIoType.Out);
    }

    private async Task<ActionResult<BaseResponse<PaymentViewModel>>> CreatePayment(
        CreatePaymentRequest request,
        PaymentType paymentType,
        PaymentIoType paymentIoType
    )
    {
        CreatePaymentCommand createPaymentCommand = Mapper.Map<CreatePaymentCommand>(request);
        createPaymentCommand.PaymentType = paymentType;
        createPaymentCommand.PaymentIoType = paymentIoType;

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