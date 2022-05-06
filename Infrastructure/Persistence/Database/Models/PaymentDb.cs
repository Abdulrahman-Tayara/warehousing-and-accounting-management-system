using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Common.Mappings;
using Domain.Entities;

namespace Infrastructure.Persistence.Database.Models;

public class PaymentDb : IDbModel, IMapFrom<Payment>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public int InvoiceId { get; set; }
    [ForeignKey("InvoiceId")]
    public InvoiceDb? Invoice { get; set; }

    public string? Note { get; set; }

    public PaymentType PaymentType { get; set; }

    public PaymentIoType PaymentIoType { get; set; }
    
    public double Amount { get; set; }
    
    public int CurrencyId { get; set; }
    [ForeignKey("CurrencyId")]
    public Currency? Currency { get; set; }
    
    [ForeignKey("ObjectId")]
    public IEnumerable<CurrencyAmount> CurrencyAmounts { get; set; }

    public DateTime CreatedAt { get; set; }
}