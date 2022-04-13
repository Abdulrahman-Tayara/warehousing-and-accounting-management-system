using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Common.Mappings;

namespace Infrastructure.Persistence.Database.Models;

[Table("Manufacturers")]
public class ManufacturerDb : IMapFrom<Domain.Entities.Manufacturer>, IDbModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } = default;
    
    [Required]
    public string Name { get; set; } = default!;
    
    public string? Code { get; set; }
}