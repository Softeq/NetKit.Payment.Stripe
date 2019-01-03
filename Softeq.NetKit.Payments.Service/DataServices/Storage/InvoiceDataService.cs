// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Softeq.NetKit.Payments.Data.Models.Invoice;
using Softeq.NetKit.Payments.Service.DataServices.Abstract;
using Softeq.NetKit.Payments.Service.Exceptions;
using Softeq.NetKit.Payments.Service.Utility.ErrorHandling;
using Softeq.NetKit.Payments.SQLRepository.Interfaces;

namespace Softeq.NetKit.Payments.Service.DataServices.Storage
{
    public class InvoiceDataService : BaseService, IInvoiceDataService
    {
        public InvoiceDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <inheritdoc />
        public async Task<List<Invoice>> UserInvoicesAsync(string userId)
        {
            var invoices = await UnitOfWork.InvoiceRepository.Query(x => x.SaasUserId == userId)
                .ToListAsync();
            return invoices;
        }

        /// <inheritdoc />
        public async Task<Invoice> UserInvoiceAsync(string userId, string invoiceId)
        {
            var invoice = await UnitOfWork.InvoiceRepository.Query(x => x.SaasUserId == userId && x.StripeId == invoiceId)
                .FirstOrDefaultAsync();
            if (invoice == null)
            {
                throw new NotFoundException(new ErrorDto(ErrorCode.NotFound, "Invoice does not exist."));
            }

            return invoice;
        }

        /// <inheritdoc />
        public async Task CreateOrUpdateAsync(Invoice invoice)
        {
            var dbInvoice = await UnitOfWork.InvoiceRepository.Query(x => x.StripeId == invoice.StripeId)
                .FirstOrDefaultAsync();

            if (dbInvoice == null)
            {
                UnitOfWork.InvoiceRepository.Add(invoice);
                await UnitOfWork.SaveChangesAsync();
            }
            else
            {
                dbInvoice.Paid = invoice.Paid;
                dbInvoice.Attempted = invoice.Attempted;
                dbInvoice.AttemptCount = invoice.AttemptCount;
                dbInvoice.NextPaymentAttempt = invoice.NextPaymentAttempt;
                dbInvoice.Closed = invoice.Closed;
                UnitOfWork.InvoiceRepository.Update(dbInvoice);
                await UnitOfWork.SaveChangesAsync();
            }
        }

        /// <inheritdoc />
        public async Task<List<Invoice>> GetInvoicesAsync()
        {
            var invoices = await UnitOfWork.InvoiceRepository.GetAll()
                .Include(x => x.Customer)
                .ToListAsync();
            return invoices;
        }
    }
}