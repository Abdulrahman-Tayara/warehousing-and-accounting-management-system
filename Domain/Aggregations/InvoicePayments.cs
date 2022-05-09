using Domain.Entities;
using Domain.Exceptions;

namespace Domain.Aggregations;

public class InvoicePayments : AggregateRoot
{
    public Invoice Invoice { get; set; }
    
    public IEnumerable<Payment> Payments { get; set; }

    public IList<Payment> PendingPayments { get; } = new List<Payment>();

    public void AddPayment(Payment payment)
    {
        if (Invoice.IsClosed())
        {
            throw new InvoiceClosedException();
        }
        
        if (!IsCompatibleWithPaymentType(payment.PaymentIoType))
        {
            throw new IncompatiblePaymentIoTypeException();
        }


        double paymentsSum = Payments.Sum(p => p.Amount);

        if (payment.Amount + paymentsSum > Invoice.TotalPrice)
        {
            throw new OverPayedException();
        }

        PendingPayments.Add(payment);
    }
    
    private bool IsCompatibleWithPaymentType(PaymentIoType paymentIoType)
    {
        return paymentIoType switch
        {
            PaymentIoType.In => Invoice.Type == InvoiceType.Out,
            PaymentIoType.Out => Invoice.Type == InvoiceType.In,
            _ => throw new NotImplementedException()
        };
    }
}