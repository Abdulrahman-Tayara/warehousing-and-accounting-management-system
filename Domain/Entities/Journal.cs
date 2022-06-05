namespace Domain.Entities;

public class Journal : BaseEntity<int>
{
    public int SourceAccountId { get; set; }

    public int AccountId { get; set; }

    public double Debit { get; set; }

    public double Credit { get; set; }

    public int CurrencyId { get; set; }
    public Currency? Currency { get; set; }

    public Journal(int id, int sourceAccountId, int accountId, double debit, double credit, int currencyId)
    {
        Id = id;
        SourceAccountId = sourceAccountId;
        AccountId = accountId;
        Debit = debit;
        Credit = credit;
        CurrencyId = currencyId;
    }
}