// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using Softeq.NetKit.Payments.Service.TransportModels.Charge.Response;
using Stripe;

namespace Softeq.NetKit.Payments.Service.TransportModels.Mappers
{
    public static class ChargeMapper
    {
        public static ChargeResponse ToChargeResponse(this StripeCharge charge, Guid userId)
        {
            var chargeResponse = new ChargeResponse();
            if (charge != null)
            {
                chargeResponse.UserId = userId;
                chargeResponse.Amount = charge.Amount;
                chargeResponse.CreditCardId = charge.Source.Id;
                chargeResponse.Currency = charge.Currency;
                chargeResponse.Date = charge.Created;
                chargeResponse.Description = charge.Description;
                chargeResponse.StripeId = charge.Id;
            }

            return chargeResponse;
        }

        public static ChargeDetailsResponse ToChargeDetailsResponse(this Data.Models.Charge.Charge charge)
        {
            var chargeResponse = new ChargeDetailsResponse();
            if (charge != null)
            {
                chargeResponse.Id = charge.Id;
                chargeResponse.UserId = charge.UserId;
                chargeResponse.Amount = charge.Amount;
                chargeResponse.CreditCardId = charge.CreditCardId;
                chargeResponse.Currency = charge.Currency;
                chargeResponse.Date = charge.Date;
                chargeResponse.Description = charge.Description;
                chargeResponse.StripeId = charge.StripeId;
                chargeResponse.SaasUserId = charge.SaasUserId;
            }

            return chargeResponse;
        }
    }
}