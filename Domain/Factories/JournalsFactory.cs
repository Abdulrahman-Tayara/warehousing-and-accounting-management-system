using Domain.Entities;

namespace Domain.Factories;

public class JournalsFactory
{
    public (Journal Debit, Journal Credit) CreateJournals(
        int sourceAccountId,
        int destinationAccountId,
        double value,
        int currencyId
    )
    {
        return (
            Debit: new Journal(
                id: default,
                sourceAccountId: destinationAccountId,
                accountId: sourceAccountId,
                debit: value,
                credit: 0,
                currencyId: currencyId
            ),
            Credit: new Journal(
                id: default,
                sourceAccountId: sourceAccountId,
                accountId: destinationAccountId,
                debit: 0,
                credit: value,
                currencyId: currencyId
            )
        );
    }
}