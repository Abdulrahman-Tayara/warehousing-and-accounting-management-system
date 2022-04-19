using Application.Common.Dtos;

namespace Application.Commands.Invoicing.Dto;

public class InvoiceItemDto
{
    public  int ProductId { get; set; }
    public double UnitPrice { get; set; }
    public int Quantity { get; set; }
    public int CurrencyId { get; set; }
    public int PlaceId { get; set; }
    public IEnumerable<CurrencyAmountDto> CurrencyAmounts { get; set; }
}