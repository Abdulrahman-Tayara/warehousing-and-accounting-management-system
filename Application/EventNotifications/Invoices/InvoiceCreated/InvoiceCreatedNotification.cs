using Domain.Entities;
using MediatR;

namespace Application.EventNotifications.Invoices.InvoiceCreated;

public class InvoiceCreatedNotification : INotification
{
    public readonly Invoice Invoice;

    public InvoiceCreatedNotification(Invoice invoice)
    {
        Invoice = invoice;
    }
}