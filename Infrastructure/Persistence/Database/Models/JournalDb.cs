using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Common.Mappings;
using Domain.Entities;

namespace Infrastructure.Persistence.Database.Models;

public class JournalDb : IDbModel, IMapFrom<Journal>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public int SourceAccountId { get; set; }
    [ForeignKey("SourceAccountId")]
    public AccountDb? SourceAccount { get; set; }

    public int AccountId { get; set; }
    [ForeignKey("AccountId")]
    public AccountDb? Account { get; set; }

    public double Debit { get; set; }
    
    public double Credit { get; set; }
    
    public int CurrencyId { get; set; }
    [ForeignKey("CurrencyId")]
    public CurrencyDb? Currency { get; set; }
}