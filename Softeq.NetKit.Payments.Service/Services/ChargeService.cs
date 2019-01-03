// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Softeq.NetKit.Payments.Data.Models.Charge;
using Softeq.NetKit.Payments.Service.DataServices.Abstract;
using Softeq.NetKit.Payments.Service.Exceptions;
using Softeq.NetKit.Payments.Service.PaymentProcessor.Interfaces;
using Softeq.NetKit.Payments.Service.Services.Abstract;
using Softeq.NetKit.Payments.Service.TransportModels.Charge.Request;
using Softeq.NetKit.Payments.Service.TransportModels.Charge.Response;
using Softeq.NetKit.Payments.Service.TransportModels.Mappers;
using Softeq.NetKit.Payments.Service.Utility.ErrorHandling;
using Stripe;

namespace Softeq.NetKit.Payments.Service.Services
{
    public class ChargeService : IChargeService
    {
        private readonly IChargeProvider _chargeProvider;
        private readonly IUserDataService _userDataService;
        private readonly IChargeDataService _chargeDataService;
        private readonly ICardDataService _cardDataService;

        public ChargeService(
            IChargeProvider chargeProvider,
            IUserDataService userDataService,
            IChargeDataService chargeDataService,
            ICardDataService cardDataService)
        {
            _chargeProvider = chargeProvider;
            _userDataService = userDataService;
            _chargeDataService = chargeDataService;
            _cardDataService = cardDataService;
        }

        public async Task<ChargeResponse> CreateChargeAsync(CreateChargeRequest request)
        {
            try
            {
                var user = await _userDataService.GetAsync(request.UserId);
                var charge = await _chargeProvider.CreateChargeAsync(request.Amount, request.Currency, request.Description, request.CardSourceId, user.StripeCustomerId);
                var card = await _cardDataService.FindAsync(request.UserId, request.CardSourceId);
                var newCharge = new Charge
                {
                    Id = Guid.NewGuid(),
                    Amount = request.Amount,
                    CreditCardId = card.Id,
                    Currency = request.Currency,
                    Date = DateTime.UtcNow,
                    Description = request.Description,
                    StripeId = charge.Id,
                    UserId = user.Id,
                    SaasUserId = request.UserId
                };
                await _chargeDataService.AddAsync(newCharge);
                return charge.ToChargeResponse(user.Id);
            }
            catch (StripeException ex)
            {
                throw new ServiceException(new ErrorDto(ex.StripeError.Code, ex.StripeError.Message));
            }
        }

        public async Task<IEnumerable<ChargeDetailsResponse>> GetAllUserCharges(string userId)
        {
            var charges = await _chargeDataService.GetAllUserCharges(userId);
            return charges.Select(x => x.ToChargeDetailsResponse());
        }
    }
}