using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Common.Mappings;
using Domain.Entities;

namespace Infrastructure.Persistence.Database.Models;

[Table("CurrencyAmounts")]
public class CurrencyAmountDb : IDbModel, IMapFrom<CurrencyAmount>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public CurrencyAmountKey Key { get; set; }
    
    public double Amount { get; set; }
    
    public int CurrencyId { get; set; }
    public CurrencyDb Currency { get; set; }
}