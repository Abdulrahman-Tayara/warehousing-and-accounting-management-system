using Application.Common.Mappings;
using Domain.Entities;

namespace Infrastructure.Persistence.Database.Models;

public class JournalDb : IDbModel, IMapFrom<Journal>
{
    
    public int Id { get; set; }
    
    public int SourceAccountId { get; set; }

    public int AccountId { get; set; }

    public double Debit { get; set; }
    
    public double Credit { get; set; }
    
    public int CurrencyId { get; set; }
}