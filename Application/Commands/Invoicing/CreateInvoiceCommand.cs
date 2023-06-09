﻿using Application.Commands.Invoicing.Dto;
using Application.Common.Security;
using Application.EventNotifications.Invoices.InvoiceCreated;
using Application.Queries.Invoicing;
using Application.Queries.Invoicing.Dto;
using Application.Repositories.UnitOfWork;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Invoicing;

[Authorize(Method = Method.Write, Resource = Resource.Invoices)]
public class CreateInvoiceCommand : IRequest<int>
{
    public int AccountId { get; set; }
    public int WarehouseId { get; set; }
    public int CurrencyId { get; set; }
    public string? Note { get; set; }
    public InvoiceType Type { get; set; }
    public InvoiceAccountType AccountType { get; set; }
    public IEnumerable<InvoiceItemDto> Items { get; set; }

    public bool IgnoreMinLevelWarnings { get; set; }
}

public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, int>
{
    private readonly Lazy<IUnitOfWork> _unitOfWork;
    private readonly IMediator _mediator;

    public CreateInvoiceCommandHandler(Lazy<IUnitOfWork> unitOfWork, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task<int> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        if (request.Type == InvoiceType.Out)
        {
            IRequest checkProductQuantityQuery = new CheckProductQuantityQuery
            {
                ProductQuantities = request.Items
                    .Select(
                        item => new CheckProductQuantityDto
                        {
                            ProductId = item.ProductId,
                            Quantity = item.Quantity
                        }
                    ),
                IgnoreMinLevelWarnings = request.IgnoreMinLevelWarnings
            };

            await _mediator.Send(checkProductQuantityQuery, cancellationToken);
        }

        var invoice = new Invoice(
            accountId: request.AccountId,
            warehouseId: request.WarehouseId,
            currencyId: request.CurrencyId,
            note: request.Note,
            createdAt: DateTime.Now,
            type: request.Type,
            accountType: request.AccountType,
            items: request.Items.Select(dto => _buildItem(dto, request.Type)).ToList()
        );

        using (var unitOfWork = _unitOfWork.Value)
        {
            var saveInvoiceAction = await unitOfWork.InvoiceRepository.CreateAsync(invoice);
            invoice = await saveInvoiceAction.Invoke();

            await _mediator.Publish(new InvoiceCreatedNotification(invoice), cancellationToken);
            
            await unitOfWork.CommitAsync();
        }

        return invoice.Id;
    }

    private static ProductMovement _buildItem(InvoiceItemDto dto, InvoiceType invoiceType)
    {
        return new ProductMovement
        {
            ProductId = dto.ProductId,
            PlaceId = dto.PlaceId,
            Quantity = dto.Quantity,
            UnitPrice = dto.UnitPrice,
            CurrencyId = dto.CurrencyId,
            Note = dto.Note,
            Type = ProductMovement.TypeFromInvoice(invoiceType),
            CreatedAt = DateTime.Now,
            CurrencyAmounts = dto.CurrencyAmounts?.Select(currencyAmountDto => new CurrencyAmount
            {
                Amount = currencyAmountDto.Value,
                CurrencyId = currencyAmountDto.CurrencyId,
                Key = CurrencyAmountKey.Movement
            })
        };
    }
}