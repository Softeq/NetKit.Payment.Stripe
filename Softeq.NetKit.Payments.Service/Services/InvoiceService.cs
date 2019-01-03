// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Softeq.NetKit.Payments.Data.Models.Invoice;
using Softeq.NetKit.Payments.Service.DataServices.Abstract;
using Softeq.NetKit.Payments.Service.Services.Abstract;
using Softeq.NetKit.Payments.Service.TransportModels.Invoice.Request;
using Softeq.NetKit.Payments.Service.TransportModels.Invoice.Response;
using Softeq.NetKit.Payments.Service.TransportModels.Mappers;

namespace Softeq.NetKit.Payments.Service.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceDataService _invoiceDataService;

        public InvoiceService(IInvoiceDataService invoiceDataService)
        {
            _invoiceDataService = invoiceDataService;
        }

        public async Task<InvoiceResponse> GetInvoiceByIdAsync(string userId, string invoiceId)
        {
            var invoice = await _invoiceDataService.UserInvoiceAsync(userId, invoiceId);
            return invoice.ToInvoiceResponse();
        }

        public async Task<InvoiceResponse> CreateInvoiceAsync(CreateInvoiceRequest request)
        {
            var invoice = new Invoice
            {
                Id = Guid.NewGuid(),
                AmountDue = request.AmountDue,
                AttemptCount = request.AttemptCount,
                Attempted = request.Attempted,
                Closed = request.Closed,
                Currency = request.Currency,
                StripeCustomerId = request.StripeCustomerId,
                StripeId = request.StripeId,
                Subtotal = request.Subtotal,
                Total = request.Total,
                Tax = request.Tax,
                TaxPercent = request.TaxPercent,
                PeriodStart = request.PeriodStart,
                PeriodEnd = request.PeriodEnd,
                StartingBalance = request.StartingBalance,
                EndingBalance = request.EndingBalance,
                Date = request.Created,
                Forgiven = request.Forgiven,
                Paid = request.Paid
            };
            await _invoiceDataService.CreateOrUpdateAsync(invoice);
            return invoice.ToInvoiceResponse();
        }

        public async Task<IEnumerable<InvoiceResponse>> GetAllInvoicesAsync()
        {
            var invoices = await _invoiceDataService.GetInvoicesAsync();
            return invoices.Select(x => x.ToInvoiceResponse());
        }

        public async Task<IEnumerable<InvoiceResponse>> GetAllUserInvoicesAsync(string userId)
        {
            var invoices = await _invoiceDataService.UserInvoicesAsync(userId);
            return invoices.Select(x => x.ToInvoiceResponse());
        }
    }
}