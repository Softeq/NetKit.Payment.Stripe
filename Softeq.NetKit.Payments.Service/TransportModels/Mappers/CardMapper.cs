// Developed by Softeq Development Corporation
// http://www.softeq.com

using Softeq.NetKit.Payments.Data.Models.Card;
using Softeq.NetKit.Payments.Service.TransportModels.Card.Response;
using Stripe;
using System;

namespace Softeq.NetKit.Payments.Service.TransportModels.Mappers
{
    public static class CardMapper
    {
        public static CreditCard ToCreditCard(this StripeCard card, Guid userId, string saasUserId, string stripeCustomerId)
        {
            if (card != null)
            {
                var creditCard = new CreditCard
                {
                    Id = Guid.NewGuid(),
                    SaasUserId = saasUserId,
                    Name = card.Name,
                    CardCountry = card.Country,
                    StripeId = card.Id,
                    ExpirationMonth = card.ExpirationMonth,
                    ExpirationYear = card.ExpirationYear,
                    Last4 = card.Last4,
                    StripeCustomerId = stripeCustomerId,
                    Fingerprint = card.Fingerprint,
                    UserId = userId
                };

                return creditCard;
            }

            return null;
        }

        public static CardResponse ToCardResponse(this CreditCard card, string userId)
        {
            if (card != null)
            {
                var creditCard = new CardResponse
                {
                    StripeId = card.StripeId,
                    ExpirationMonth = card.ExpirationMonth,
                    ExpirationYear = card.ExpirationYear,
                    Last4 = card.Last4,
                    SaasUserId = userId,
                    StripeCustomerId = card.StripeCustomerId,
                    Fingerprint = card.Fingerprint
                };

                return creditCard;
            }

            return null;
        }
    }
}