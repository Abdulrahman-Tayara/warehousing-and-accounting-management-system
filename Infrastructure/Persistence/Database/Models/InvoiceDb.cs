using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Common.Mappings;
using Domain.Entities;

namespace Infrastructure.Persistence.Database.Models;

public class InvoiceDb : IDbModel, IMapFrom<Invoice>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public int? AccountId { get; set; }
    [ForeignKey("AccountId")]
    public AccountDb? Account { get; set; }
    
    public int WarehouseId { get; set; }
    [ForeignKey("WarehouseId")]
    public WarehouseDb Warehouse { get; set; }
    
    public int? CurrencyId { get; set; }
    [ForeignKey("CurrencyId")]
    public CurrencyDb? Currency { get; set; }

    public double TotalPrice { get; set; }
    
    public IEnumerable<ProductMovementDb> Items { get; set; }
    
    public string? Note { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public InvoiceStatus Status { get; set; }
    
    public InvoiceType Type { get; set; }
    
    public InvoiceAccountType AccountType { get; set; }
}