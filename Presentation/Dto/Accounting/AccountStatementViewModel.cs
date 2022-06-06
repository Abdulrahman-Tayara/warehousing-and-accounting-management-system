using Application.Common.Mappings;
using Domain.Aggregations;
using wms.Dto.Accounts;
using wms.Dto.Common;
using wms.Dto.Currencies;

namespace wms.Dto.Accounting;

public class AccountStatementViewModel : IViewModel, IMapFrom<AggregateAccountStatement>
{
    public int Id => Account.Id;

    public AccountViewModel Account { get; set; }

    public IEnumerable<AccountStatementDetailViewModel> Details { get; set; }

    public double DebitSum { get; set; }

    public double CreditSum { get; set; }

    public double Result { get; set; }

    public CurrencyViewModel Currency { get; set; }
}

public class AccountStatementDetailViewModel : IViewModel, IMapFrom<AggregateAccountStatementDetail>
{
    public int Id { get; set; }

    public AccountViewModel Account { get; set; }

    public double Debit { get; set; }

    public double Credit { get; set; }

    public CurrencyViewModel Currency { get; set; }
}