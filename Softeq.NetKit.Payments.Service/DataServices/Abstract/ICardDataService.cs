// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Collections.Generic;
using System.Threading.Tasks;
using Softeq.NetKit.Payments.Data.Models.Card;
using Softeq.NetKit.Payments.Service.Exceptions;

namespace Softeq.NetKit.Payments.Service.DataServices.Abstract
{
    /// <summary>
    /// Interface for CRUD related to credit cards in the database.
    /// </summary>
    public interface ICardDataService
    {
        /// <summary>
        /// 	<para>Gets all credit cards for an user from the database.</para>
        /// </summary>
        /// <param name="userId">The user identifiers.</param>
        /// <returns></returns>
        Task<List<CreditCard>> GetAllAsync(string userId);

        /// <summary>Finds the credit card asynchronously from the database given the user identifier and credit card identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="stripeId">The stripe card identifier.</param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        Task<CreditCard> FindAsync(string userId, string stripeId);

        /// <summary>
        /// 	<para>Adds the credit card asynchronously to the database.</para>
        /// </summary>
        /// <param name="creditcard">The creditcard.</param>
        /// <returns></returns>
        Task AddAsync(CreditCard creditcard);

        /// <summary>Updates the credit card for a user in the database.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="creditCard">The credit card.</param>
        /// <returns></returns>
        Task UpdateAsync(string userId, CreditCard creditCard);

        /// <summary>Deletes the credit card from the database.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="creditCard">The credit card.</param>
        /// <returns></returns>
        Task DeleteAsync(string userId, CreditCard creditCard);

        /// <summary>Checks if there is any card existing in the DB given the card identifier and user identifier.</summary>
        /// <param name="cardId">The card identifier.</param>
        /// <param name="userId">The customer identifier.</param>
        /// <returns>bool</returns>
        Task<bool> AnyAsync(string cardId, string userId);
    }
}