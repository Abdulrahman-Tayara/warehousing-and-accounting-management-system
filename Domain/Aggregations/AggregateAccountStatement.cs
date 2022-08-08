using Domain.Entities;

namespace Domain.Aggregations;

public class AggregateAccountStatement : AggregateRoot
{
    public int Id => Account.Id;

    public Account Account { get; set; }

    public IEnumerable<AggregateAccountStatementDetail> Details { get; set; }

    public double DebitSum { get; set; }

    public double CreditSum { get; set; }

    public double Result => DebitSum - CreditSum;

    public Currency Currency { get; set; }

    public AggregateAccountStatement(Account account, IEnumerable<AggregateAccountStatementDetail> details, double debitSum,
        double creditSum,
        Currency currency)
    {
        Account = account;
        Details = details;
        DebitSum = debitSum;
        CreditSum = creditSum;
        Currency = currency;
    }
}

public class AggregateAccountStatementDetail : AggregateRoot
{
    public int Id => Account.Id;

    public Account Account { get; set; }

    public double Debit { get; set; }

    public double Credit { get; set; }

    public double Result => Debit - Credit;
    public Currency Currency { get; set; }

    public AggregateAccountStatementDetail(Account account, double debit, double credit, Currency currency)
    {
        Account = account;
        Debit = debit;
        Credit = credit;
        Currency = currency;
    }
}