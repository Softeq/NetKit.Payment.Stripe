// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Collections.Generic;
using System.Threading.Tasks;
using Softeq.NetKit.Payments.Data.Models.Invoice;
using Softeq.NetKit.Payments.Service.Exceptions;

namespace Softeq.NetKit.Payments.Service.DataServices.Abstract
{
    /// <summary>
    /// Interface for CRUD related to invoices in the database.
    /// </summary>
    public interface IInvoiceDataService
    {
        /// <summary>
        /// Gets the User's invoices asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of invoices.</returns>
        Task<List<Invoice>> UserInvoicesAsync(string userId);

        /// <summary>
        /// Gets the invoice given a users identifier and the invoice identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="invoiceId">The invoice identifier.</param>
        /// <returns>The invoice</returns>
        /// <exception cref="NotFoundException"></exception>
        Task<Invoice> UserInvoiceAsync(string userId, string invoiceId);

        /// <summary>
        /// Creates the or update asynchronous.
        /// </summary>
        /// <param name="invoice">The invoice.</param>
        /// <returns>int</returns>
        Task CreateOrUpdateAsync(Invoice invoice);

        /// <summary>
        /// Gets all the invoices asynchronous.
        /// </summary>
        /// <returns>List of invoices.</returns>
        Task<List<Invoice>> GetInvoicesAsync();
    }
}