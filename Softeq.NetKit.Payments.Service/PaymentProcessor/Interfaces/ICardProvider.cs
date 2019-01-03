// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Threading.Tasks;
using Stripe;

namespace Softeq.NetKit.Payments.Service.PaymentProcessor.Interfaces
{
    /// <summary>
    /// Interface for CRUD related to credit cards with Stripe
    /// </summary>
    public interface ICardProvider
    {
        /// <summary>
        /// Finds the credit card asynchronous.
        /// </summary>
        /// <param name="customerId">The stripe customer identifier.</param>
        /// <param name="cardId">The card identifier.</param>
        /// <returns>The credit card</returns>
        Task<StripeCard> FindAsync(string customerId, string cardId);

        /// <summary>
        /// Delete the credit card asynchronous.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="cardId">The stripe card identifier.</param>
        Task DeleteCreditCardAsync(string customerId, string cardId);

        /// <summary>
        /// Delete the credit card asynchronous.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="sourceTokenId">The source token identifier.</param>
        Task<StripeCard> CreateCreditCardAsync(string customerId, string sourceTokenId);
    }
}