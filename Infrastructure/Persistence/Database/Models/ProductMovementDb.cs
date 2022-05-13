using System.ComponentModel.DataAnnotations.Schema;
using Application.Common.Mappings;
using Domain.Entities;

namespace Infrastructure.Persistence.Database.Models;

[Table("Movements")]
public class ProductMovementDb : IDbModel, IMapFrom<ProductMovement>
{
    public int Id { get; set; }
    
    public int InvoiceId { get; set; }
    [ForeignKey("InvoiceId")]
    public InvoiceDb Invoice { get; set; }
    
    public int? ProductId { get; set; }
    [ForeignKey("ProductId")]
    public ProductDb? Product { get; set; }
    
    public int? PlaceId { get; set; }
    [ForeignKey("PlaceId")]
    public StoragePlaceDb? Place { get; set; }
    
    public int Quantity { get; set; }
    
    public double UnitPrice { get; set; }
    
    public double TotalPrice { get; set; }
    
    public int? CurrencyId { get; set; }
    [ForeignKey("CurrencyId")]
    public CurrencyDb? Currency { get; set; }
    [ForeignKey("ObjectId")]
    public IEnumerable<CurrencyAmountDb>? CurrencyAmounts { get; set; }

    public ProductMovementType Type { get; set; }
    
    public string? Note { get; set; }
    
    public DateTime CreatedAt { get; set; }
}