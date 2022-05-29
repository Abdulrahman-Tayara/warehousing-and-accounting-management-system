using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Common.Mappings;
using Domain.Entities;

namespace Infrastructure.Persistence.Database.Models;

[Table("CountryOrigins")]
public class CountryOriginDb : IDbModel, IMapFrom<CountryOrigin>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Name { get; set; }
}