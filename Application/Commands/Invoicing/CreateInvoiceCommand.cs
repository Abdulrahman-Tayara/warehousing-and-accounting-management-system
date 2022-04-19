using Application.Commands.Invoicing.Dto;
using MediatR;

namespace Application.Commands.Invoicing;

public class CreateInvoiceCommand : IRequest<int>
{
    public int AccountId { get; set; }
    public int WarehouseId { get; set; }
    public string Note { get; set; }
    // TODO: Make it an enum
    public string Type { get; set; }
    public IEnumerable<InvoiceItemDto> Items { get; set; }
}