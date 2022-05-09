using Domain.Aggregations;

namespace Application.Repositories.Aggregates;

public interface IInvoicePaymentsRepository : IRepositoryBase
{
    public Task<InvoicePayments> FindByInvoiceId(int invoiceId);

    public Task<SaveAction<Task<InvoicePayments>>> CreatePayments(InvoicePayments invoicePayments);
}