// Developed by Softeq Development Corporation
// http://www.softeq.com

using Softeq.NetKit.Payments.Service.TransportModels.Invoice.Response;

namespace Softeq.NetKit.Payments.Service.TransportModels.Mappers
{
    public static class InvoiceMapper
    {
        public static InvoiceResponse ToInvoiceResponse(this Data.Models.Invoice.Invoice invoice)
        {
            var invoiceResponse = new InvoiceResponse();
            if (invoice != null)
            {
                invoiceResponse.Id = invoice.Id;
                invoiceResponse.StripeCustomerId = invoice.StripeCustomerId;
                invoiceResponse.AmountDue = invoice.AmountDue;
                invoiceResponse.AttemptCount = invoice.AttemptCount;
                invoiceResponse.Attempted = invoice.Attempted;
                invoiceResponse.Closed = invoice.Closed;
                invoiceResponse.Created = invoice.Date;
                invoiceResponse.Currency = invoice.Currency;
                invoiceResponse.EndingBalance = invoice.EndingBalance;
                invoiceResponse.Forgiven = invoice.Forgiven;
                invoiceResponse.Paid = invoice.Paid;
                invoiceResponse.PeriodEnd = invoice.PeriodEnd;
                invoiceResponse.PeriodStart = invoice.PeriodStart;
                invoiceResponse.StartingBalance = invoice.StartingBalance;
                invoiceResponse.StripeId = invoice.StripeId;
                invoiceResponse.Subtotal = invoice.Subtotal;
                invoiceResponse.Tax = invoice.Tax;
                invoiceResponse.TaxPercent = invoice.TaxPercent;
                invoiceResponse.Total = invoice.Total;
            }

            return invoiceResponse;
        }
    }
}