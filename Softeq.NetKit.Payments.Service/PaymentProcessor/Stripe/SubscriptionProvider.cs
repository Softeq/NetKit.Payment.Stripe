// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Threading.Tasks;
using Softeq.NetKit.Payments.Service.PaymentProcessor.Interfaces;
using Stripe;

namespace Softeq.NetKit.Payments.Service.PaymentProcessor.Stripe
{
    /// <summary>
    /// Implementation for subscription management with Stripe
    /// </summary>
    public class SubscriptionProvider : ISubscriptionProvider
    {
        private readonly StripeSubscriptionService _subscriptionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionProvider"/> class.
        /// </summary>
        /// <param name="settings">Stripe configuration settings.</param>
        public SubscriptionProvider(StripeSettings settings)
        {
            _subscriptionService = new StripeSubscriptionService(settings.ApiKey);
        }

        /// <inheritdoc />
        public async Task<string> SubscribeUserAsync(string customerId, string planId, int trialInDays = 0, decimal taxPercent = 0)
        {
            var result = await _subscriptionService.CreateAsync(customerId, planId,
                new StripeSubscriptionCreateOptions
                {
                    TaxPercent = taxPercent,
                    TrialEnd = trialInDays == 0 ? (DateTime?) null : DateTime.UtcNow.AddDays(trialInDays)
                });

            return result.Id;
        }

        /// <inheritdoc />
        public async Task<DateTime> EndSubscriptionAsync(string subStripeId, bool cancelAtPeriodEnd = false)
        {
            var subscription = await _subscriptionService.CancelAsync(subStripeId, cancelAtPeriodEnd);

            return cancelAtPeriodEnd ? subscription.EndedAt ?? DateTime.UtcNow : DateTime.UtcNow;
        }

        /// <inheritdoc />
        public async Task UpdateSubscriptionAsync(string subStripeId, string newPlanId, bool proRate)
        {
            var currentSubscription = await _subscriptionService.GetAsync(subStripeId);

            var myUpdatedSubscription = new StripeSubscriptionUpdateOptions
            {
                PlanId = newPlanId,
                Prorate = proRate
            };

            if (currentSubscription.TrialEnd != null && currentSubscription.TrialEnd > DateTime.UtcNow)
            {
                myUpdatedSubscription.TrialEnd = currentSubscription.TrialEnd; // Keep the same trial window as initially created.
            }

            await _subscriptionService.UpdateAsync(subStripeId, myUpdatedSubscription);
        }

        /// <inheritdoc />
        public async Task<StripeSubscription> SubscribeUserNaturalMonthAsync(string stripeCustomerId, string planId, DateTime? billingAnchorCycle, decimal taxPercent)
        {
            var options = new StripeSubscriptionCreateOptions
            {
                BillingCycleAnchor = billingAnchorCycle,
                TaxPercent = taxPercent
            };

            var subscription = await _subscriptionService.CreateAsync(stripeCustomerId, planId, options);

            return subscription;
        }
    }
}