using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Common.Mappings;
using Domain.Entities;

namespace Infrastructure.Persistence.Database.Models;

[Table("Currencies")]
public class CurrencyDb : IDbModel, IMapFrom<Currency>
{
    [Key]
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Symbol { get; set; }
    
    public float Factor { get; set; }
}