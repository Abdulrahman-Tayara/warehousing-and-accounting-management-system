using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Common.Mappings;
using Domain.Entities;

namespace Infrastructure.Persistence.Database.Models;

[Table("StoragePlaces")]
public class StoragePlaceDb : IDbModel, IMapFrom<StoragePlace>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public int WarehouseId { get; set; }
    [ForeignKey("WarehouseId")]
    public WarehouseDb? Warehouse { get; set; } 
    
    public int? ContainerId { get; set; }
    [ForeignKey("ContainerId")]
    public StoragePlaceDb? Container { get; set; }
}