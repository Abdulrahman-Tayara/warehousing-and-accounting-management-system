using MediatR;

using Domain.Entities;

namespace Application.EventNotifications.Invoices;

public class InvoiceCreatedNotification : INotification
{
    public readonly Invoice Invoice;

    public InvoiceCreatedNotification(Invoice invoice)
    {
        Invoice = invoice;
    }
}