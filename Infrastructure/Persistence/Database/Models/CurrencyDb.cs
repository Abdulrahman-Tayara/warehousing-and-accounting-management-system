using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Common.Mappings;
using Domain.Entities;

namespace Infrastructure.Persistence.Database.Models;

[Table("Currencies")]
public class CurrencyDb : IDbModel, IMapFrom<Currency>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } = default;
    
    public string Name { get; set; } = default!;
    
    public string Symbol { get; set; } = default!;
    
    public float Factor { get; set; } = default;
}