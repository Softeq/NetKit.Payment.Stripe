// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Threading.Tasks;
using Softeq.NetKit.Payments.Service.PaymentProcessor.Interfaces;
using Stripe;

namespace Softeq.NetKit.Payments.Service.PaymentProcessor.Stripe
{
    /// <summary>
    /// Implementation for CRUD related to credit cards with Stripe and also saves the details in the database. 
    /// </summary>
    public class CardProvider : ICardProvider
    {
        private readonly StripeCardService _cardService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardProvider"/> class.
        /// </summary>
        /// <param name="settings">Stripe configuration settings.</param>
        public CardProvider(StripeSettings settings)
        {
            _cardService = new StripeCardService(settings.ApiKey);
        }

        /// <inheritdoc />
        public async Task<StripeCard> FindAsync(string customerId, string cardId)
        {
            var card = await _cardService.GetAsync(customerId, cardId);
            return card;
        }

        /// <inheritdoc />
        public async Task DeleteCreditCardAsync(string customerId, string cardId)
        {
            await _cardService.DeleteAsync(customerId, cardId);
        }

        /// <inheritdoc />
        public async Task<StripeCard> CreateCreditCardAsync(string customerId, string sourceTokenId)
        {
            var options = new StripeCardCreateOptions
            {
                SourceToken = sourceTokenId
            };
            var card = await _cardService.CreateAsync(customerId, options);
            return card;
        }
    }
}