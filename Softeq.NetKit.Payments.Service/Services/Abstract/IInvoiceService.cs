// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Collections.Generic;
using System.Threading.Tasks;
using Softeq.NetKit.Payments.Service.TransportModels.Invoice.Request;
using Softeq.NetKit.Payments.Service.TransportModels.Invoice.Response;

namespace Softeq.NetKit.Payments.Service.Services.Abstract
{
    public interface IInvoiceService
    {
        Task<InvoiceResponse> GetInvoiceByIdAsync(string userId, string invoiceId);
        Task<InvoiceResponse> CreateInvoiceAsync(CreateInvoiceRequest request);
        Task<IEnumerable<InvoiceResponse>> GetAllInvoicesAsync();
        Task<IEnumerable<InvoiceResponse>> GetAllUserInvoicesAsync(string userId);
    }
}