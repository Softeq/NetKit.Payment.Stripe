// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Softeq.NetKit.Payments.Service.DataServices.Abstract;
using Softeq.NetKit.Payments.Service.Exceptions;
using Softeq.NetKit.Payments.Service.PaymentProcessor.Interfaces;
using Softeq.NetKit.Payments.Service.Services.Abstract;
using Softeq.NetKit.Payments.Service.TransportModels.Card.Response;
using Softeq.NetKit.Payments.Service.TransportModels.Mappers;
using Softeq.NetKit.Payments.Service.Utility.ErrorHandling;
using Stripe;

namespace Softeq.NetKit.Payments.Service.Services
{
    public class CardService : ICardService
    {
        private readonly ICardProvider _cardProvider;
        private readonly ITokenProvider _tokenProvider;
        private readonly IUserDataService _userDataService;
        private readonly ICardDataService _cardDataService;

        public CardService(ICardProvider cardProvider, IUserDataService userDataService, ICardDataService cardDataService, ITokenProvider tokenProvider)
        {
            _cardProvider = cardProvider;
            _userDataService = userDataService;
            _cardDataService = cardDataService;
            _tokenProvider = tokenProvider;
        }

        /// <summary>
        /// Get the default payment credit card for a user.
        /// </summary>
        /// <param name="userId">Application User PlanId.</param>
        /// <param name="cardId">The card identifier.</param>
        /// <returns>Credit Card or Null</returns>
        public async Task<CardResponse> GetCreditCardByIdAsync(string userId, string cardId)
        {
            var card = await _cardDataService.FindAsync(userId, cardId);
            return card.ToCardResponse(userId);
        }

        public async Task AddCreditCardToDbAsync(string userId, string sourceTokenId)
        {
            try
            {
                // Attach credit card to customer in Stripe
                var user = await _userDataService.GetAsync(userId);
                // Get token from Stripe
                var token = await _tokenProvider.GetTokenAsync(sourceTokenId);
                if (token.StripeCard == null)
                {
                    throw new NotFoundException(new ErrorDto(ErrorCode.NotFound, "Credit card does not exist."));
                }

                var stripeCard = await _cardProvider.CreateCreditCardAsync(user.StripeCustomerId, sourceTokenId);

                // Add credit card in database
                var checkIfCreditCardExist = await _cardDataService.AnyAsync(stripeCard.Id, userId);
                if (!checkIfCreditCardExist)
                {
                    var creditCard = stripeCard.ToCreditCard(user.Id, userId, user.StripeCustomerId);
                    await _cardDataService.AddAsync(creditCard);
                }
            }
            catch (StripeException ex)
            {
                throw new ServiceException(new ErrorDto(ex.StripeError.Code, ex.StripeError.Message));
            }
        }

        public async Task DeleteCreditCardAsync(string userId, string cardId)
        {
            try
            {
                var user = await _userDataService.GetAsync(userId);
                var stripeCard = await _cardProvider.FindAsync(user.StripeCustomerId, cardId);
                if (stripeCard == null)
                {
                    throw new NotFoundException(new ErrorDto(ErrorCode.NotFound, "Credit card does not exist."));
                }

                // Delete credit card from stripe
                await _cardProvider.DeleteCreditCardAsync(user.StripeCustomerId, cardId);

                // Delete credit card from DB
                var card = await _cardDataService.FindAsync(userId, stripeCard.Id);
                await _cardDataService.DeleteAsync(userId, card);
            }
            catch (StripeException ex)
            {
                throw new ServiceException(new ErrorDto(ex.StripeError.Code, ex.StripeError.Message));
            }
        }

        public async Task<IEnumerable<CardResponse>> GetCreditCardsAsync(string userId)
        {
            var newCards = await _cardDataService.GetAllAsync(userId);
            return newCards.Select(x => x.ToCardResponse(userId));
        }
    }
}