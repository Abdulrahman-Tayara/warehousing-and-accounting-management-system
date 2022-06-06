using Domain.Entities;
using MediatR;

namespace Application.EventNotifications.Payments.PaymentCreated;

public class PaymentCreatedNotification : INotification
{
    public readonly Payment Payment;

    public PaymentCreatedNotification(Payment payment)
    {
        Payment = payment;
    }
}