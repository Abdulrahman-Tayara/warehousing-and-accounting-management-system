using Application.Repositories;
using Application.Repositories.Aggregates;
using Domain.Aggregations;
using Domain.Entities;
using Infrastructure.Persistence.Database;

namespace Infrastructure.Repositories.Aggregates;

public class InvoicePaymentsRepository : RepositoryBase<ApplicationDbContext>, IInvoicePaymentsRepository
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IPaymentRepository _paymentRepository;

    public InvoicePaymentsRepository(
        ApplicationDbContext dbContext,
        IInvoiceRepository invoiceRepository,
        IPaymentRepository paymentRepository
    ) : base(dbContext)
    {
        _invoiceRepository = invoiceRepository;
        _paymentRepository = paymentRepository;
    }

    public async Task<InvoicePayments> FindByInvoiceId(int invoiceId)
    {
        var invoice = await _invoiceRepository.FindByIdAsync(invoiceId);
        var payments = _paymentRepository
            .GetAll()
            .Where(p => p.InvoiceId == invoiceId)
            .ToList();

        return new InvoicePayments
        {
            Invoice = invoice,
            Payments = payments
        };
    }

    public async Task<SaveAction<Task<InvoicePayments>>> Save(InvoicePayments invoicePayments)
    {
        var saveAction = await _paymentRepository.CreateAllAsync(invoicePayments.PendingPayments);

        return async () =>
        {
            var savedPayments = await saveAction();

            Invoice updatedInvoice = await _invoiceRepository.Update(invoicePayments.Invoice);

            return new InvoicePayments
            {
                Invoice = updatedInvoice,
                Payments = invoicePayments.Payments.Concat(savedPayments)
            };
        };
    }
}