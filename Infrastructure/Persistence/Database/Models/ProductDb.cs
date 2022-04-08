using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Common.Mappings;
using Domain.Entities;

namespace Infrastructure.Persistence.Database.Models;

[Table("Products")]
public class ProductDb : IDbModel, IMapFrom<Product>
{
    [Key]
    public int Id { get; set; }
    
    public string Name { get; set; }

    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public CategoryDb Category { get; set; }

    public int ManufacturerId { get; set; }
    [ForeignKey("ManufacturerId")]
    public ManufacturerDb Manufacturer { get; set; }

    public int UnitId { get; set; }
    [ForeignKey("UnitId")]
    public UnitDb Unit { get; set; }

    public string Barcode { get; set; }

    public double Price { get; set; }
    
    public int CurrencyId { get; set; }
    [ForeignKey("CurrencyId")]
    public CurrencyDb Currency { get; set; }
}