// Developed by Softeq Development Corporation
// http://www.softeq.com

using Microsoft.Extensions.Configuration;

namespace Softeq.NetKit.Payments.Service.PaymentProcessor.Stripe
{
    public class StripeSettings
    {
        public StripeSettings(IConfiguration configuration)
        {
            ApiKey = configuration["Stripe:ApiKey"];
        }

        /// <summary>
        /// The API key.
        /// </summary>
        public string ApiKey { get; }
    }
}