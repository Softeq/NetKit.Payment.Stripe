// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Threading.Tasks;
using Softeq.NetKit.Payments.Service.PaymentProcessor.Interfaces;
using Stripe;

namespace Softeq.NetKit.Payments.Service.PaymentProcessor.Stripe
{
    public class TokenProvider : ITokenProvider
    {
        private readonly StripeTokenService _stripeTokenService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenProvider"/> class.
        /// </summary>
        /// <param name="settings">Stripe configuration settings.</param>
        public TokenProvider(StripeSettings settings)
        {
            _stripeTokenService = new StripeTokenService(settings.ApiKey);
        }

        public async Task<StripeToken> GetTokenAsync(string tokenId)
        {
            return await _stripeTokenService.GetAsync(tokenId);
        }
    }
}