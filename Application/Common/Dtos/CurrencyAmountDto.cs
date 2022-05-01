using System.ComponentModel.DataAnnotations;

namespace Application.Common.Dtos;

public class CurrencyAmountDto
{
    [Required]
    public int CurrencyId { get; set; }
    [Required]
    public double Value { get; set; }
}