using System.ComponentModel.DataAnnotations.Schema;
using Application.Common.Mappings;
using Domain.Entities;

namespace Infrastructure.Persistence.Database.Models;

[Table("CurrencyAmounts")]
public class CurrencyAmountDb : IDbModel, IMapFrom<CurrencyAmount>
{
    public int Id { get; set; }
    
    public int? ObjectId { get; set; }
    
    public CurrencyAmountKey Key { get; set; }
    
    public double Amount { get; set; }
    
    public int CurrencyId { get; set; }
    public CurrencyDb Currency { get; set; }
}