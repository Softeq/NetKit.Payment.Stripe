// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Softeq.NetKit.Payments.Service.PaymentProcessor.Interfaces;
using Stripe;

namespace Softeq.NetKit.Payments.Service.PaymentProcessor.Stripe
{
    /// <summary>
    /// Interface for CRUD related to customers with Stripe
    /// </summary>
    public class CustomerProvider : ICustomerProvider
    {
        // Stripe Dependencies
        private readonly StripeCustomerService _customerService;
        private readonly StripeEphemeralKeyService _ephemeralKeyService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerProvider"/> class.
        /// </summary>
        /// <param name="settings">Stripe configuration settings.</param>
        public CustomerProvider(StripeSettings settings)
        {
            _customerService = new StripeCustomerService(settings.ApiKey);
            _ephemeralKeyService = new StripeEphemeralKeyService(settings.ApiKey);
        }

        public async Task<StripeCustomer> CreateCustomerAsync(string email)
        {
            var options = new StripeCustomerCreateOptions
            {
                AccountBalance = 0,
                Email = email
            };

            var stripeUser = await _customerService.CreateAsync(options);
            return stripeUser;
        }

        public async Task<object> CreateEphemeralKeyAsync(string stripeCustomerId, string userId, string email, string stripeVersion)
        {
            var ephemeralKeysOptions = new StripeEphemeralKeyCreateOptions
            {
                CustomerId = stripeCustomerId,
                StripeVersion = stripeVersion
            };
            var ephemeralKey = await _ephemeralKeyService.CreateAsync(ephemeralKeysOptions);
            return JObject.Parse(ephemeralKey.RawJson);
        }

        public async Task<IEnumerable<StripeSubscription>> GetUserActiveSubscriptionsAsync(string customerId)
        {
            var subscriptions = (await _customerService.GetAsync(customerId))
                .Subscriptions.Data.Where(x => x.CustomerId == customerId &&
                                               x.Status != "canceled" && x.Status != "unpaid" &&
                                               (x.TrialEnd == null || x.TrialEnd > DateTime.UtcNow));
            return subscriptions;
        }
    }
}