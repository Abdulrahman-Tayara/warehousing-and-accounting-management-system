using System.ComponentModel.DataAnnotations;
using Application.Common.Dtos;

namespace Application.Commands.Invoicing.Dto;

public class InvoiceItemDto
{
    [Required]
    public  int ProductId { get; set; }
    [Required]
    public double UnitPrice { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public int CurrencyId { get; set; }
    [Required]
    public int PlaceId { get; set; }
    public string? Note { get; set; }
    public IEnumerable<CurrencyAmountDto>? CurrencyAmounts { get; set; }
    public bool HasCurrencyAmount =>  CurrencyAmounts != null && CurrencyAmounts.Any();
}