// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Threading.Tasks;
using Softeq.NetKit.Payments.Service.PaymentProcessor.Interfaces;
using Stripe;

namespace Softeq.NetKit.Payments.Service.PaymentProcessor.Stripe
{
    /// <summary>
    /// Implementation for CRUD related to charges with Stripe
    /// </summary>
    public class ChargeProvider : IChargeProvider
    {
        // Stripe Dependencies
        private readonly StripeChargeService _chargeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChargeProvider"/> class.
        /// </summary>
        /// <param name="settings">Stripe configuration settings.</param>
        public ChargeProvider(StripeSettings settings)
        {
            _chargeService = new StripeChargeService(settings.ApiKey);
        }

        /// <inheritdoc />
        public async Task<StripeCharge> CreateChargeAsync(int amount, string currency, string description, string creditSourceId, string customerId)
        {
            var options = new StripeChargeCreateOptions
            {
                Amount = amount,
                Currency = currency,
                Description = description,
                SourceTokenOrExistingSourceId = creditSourceId,
                CustomerId = customerId
            };

            var charge = await _chargeService.CreateAsync(options);
            return charge;
        }
    }
}