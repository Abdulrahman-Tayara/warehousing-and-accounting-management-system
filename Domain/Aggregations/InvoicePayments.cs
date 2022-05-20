using Domain.Entities;
using Domain.Exceptions;

namespace Domain.Aggregations;

public class InvoicePayments : AggregateRoot
{
    public Invoice Invoice { get; set; } = default!;

    public IEnumerable<Payment> Payments { get; set; } = default!;

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

        if (AddedPaymentOverpaysInvoice(payment))
        {
            throw new OverPayedException();
        }

        PendingPayments.Add(payment);

        if (AddedPaymentWouldCloseInvoice(payment))
        {
            Invoice.Close();
        }
    }

    private bool AddedPaymentOverpaysInvoice(Payment payment)
    {
        double paymentsSum = Payments.Sum(p => p.Amount);
        
        return payment.Amount + paymentsSum > Invoice.TotalPrice;
    }

    private bool AddedPaymentWouldCloseInvoice(Payment payment)
    {
        double paymentsSum = Payments.Sum(p => p.Amount);
        
        // Direct comparison will make loss of precision
        return Math.Abs(payment.Amount + paymentsSum - Invoice.TotalPrice) < 0.001;
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