// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Collections.Generic;
using System.Threading.Tasks;
using Softeq.NetKit.Payments.Service.TransportModels.Card.Response;

namespace Softeq.NetKit.Payments.Service.Services.Abstract
{
    public interface ICardService
    {
        /// <summary>
        /// Get the default payment credit card for a user.
        /// </summary>
        /// <param name="userId">Application User PlanId.</param>
        /// <param name="cardId">The card identifier.</param>
        /// <returns>Credit Card or Null</returns>
        Task<CardResponse> GetCreditCardByIdAsync(string userId, string cardId);

        Task AddCreditCardToDbAsync(string userId, string sourceTokenId);

        Task DeleteCreditCardAsync(string userId, string cardId);

        Task<IEnumerable<CardResponse>> GetCreditCardsAsync(string userId);
    }
}