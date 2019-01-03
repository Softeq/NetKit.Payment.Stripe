// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using Softeq.NetKit.Payments.Data.Models.Invoice;

namespace Softeq.NetKit.Payments.SQLRepository.Interfaces
{
    public interface IInvoiceRepository : IRepository<Invoice, Guid>
    {
    }
}