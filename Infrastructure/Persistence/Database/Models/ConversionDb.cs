using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Common.Mappings;
using Domain.Entities;

namespace Infrastructure.Persistence.Database.Models;

[Table("Conversions")]
public class ConversionDb : IDbModel, IMapFrom<Conversion>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public int FromWarehouseId { get; set; }
    [ForeignKey("FromWarehouseId")]
    public WarehouseDb? FromWarehouse { get; set; }
    
    public int ToWarehouseId { get; set; }
    [ForeignKey("ToWarehouseId")]
    public WarehouseDb? ToWarehouse { get; set; }
    
    public int FromProductId { get; set; }
    [ForeignKey("FromProductId")]
    public ProductDb? FromProduct { get; set; }
    
    public int ToProductId { get; set; }
    [ForeignKey("ToProductId")]
    public ProductDb? ToProduct { get; set; }
    
    public int FromPlaceId { get; set; }
    [ForeignKey("FromPlaceId")]
    public StoragePlaceDb? FromPlace { get; set; }
    
    public int ToPlaceId { get; set; }
    [ForeignKey("ToPlaceId")]
    public StoragePlaceDb? ToPlace { get; set; }
    
    public int FromQuantity { get; set; }
    
    public int ToQuantity { get; set; }
    
    public string? Note { get; set; }
    
    public int ExportInvoiceId { get; set; }
    [ForeignKey("ExportInvoiceId")]
    public InvoiceDb? ExportInvoice { get; set; }
    
    public int ImportInvoiceId { get; set; }
    [ForeignKey("ImportInvoiceId")]
    public InvoiceDb? ImportInvoice { get; set; }
}