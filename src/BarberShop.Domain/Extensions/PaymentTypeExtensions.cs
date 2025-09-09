using BarberShop.Domain.Enums;
using BarberShop.Domain.Reports;

namespace BarberShop.Domain.Extensions;
public static class PaymentTypeExtensions
{
    public static string PaymentTypeToString(this PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.Cash => ResourceReportPaymentTypeMessages.CASH,
            PaymentType.CreditCard => ResourceReportPaymentTypeMessages.CREDIT_CARD,
            PaymentType.DebitCard => ResourceReportPaymentTypeMessages.DEBIT_CARD,
            PaymentType.EletronicTransfer => ResourceReportPaymentTypeMessages.ELETRONIC_TRANSFER,
            _ => string.Empty
        };
    }
}
