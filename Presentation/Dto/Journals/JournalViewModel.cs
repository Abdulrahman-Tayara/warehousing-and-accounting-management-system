using Application.Common.Mappings;
using Domain.Entities;
using wms.Dto.Accounts;
using wms.Dto.Common;
using wms.Dto.Currencies;

namespace wms.Dto.Journals;

public class JournalViewModel : IViewModel, IMapFrom<Journal>
{
    public int SourceAccountId { get; set; }
    public AccountViewModel? SourceAccount { get; set; }

    public int AccountId { get; set; }
    public AccountViewModel? Account { get; set; }

    public double Debit { get; set; }

    public double Credit { get; set; }

    public int CurrencyId { get; set; }
    public CurrencyViewModel? Currency { get; set; }
}