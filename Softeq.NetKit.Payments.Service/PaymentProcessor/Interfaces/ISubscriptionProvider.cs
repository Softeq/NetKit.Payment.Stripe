// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Threading.Tasks;
using Stripe;

namespace Softeq.NetKit.Payments.Service.PaymentProcessor.Interfaces
{
    /// <summary>
    /// Interface for subscription management with Stripe
    /// </summary>
    public interface ISubscriptionProvider
    {
        /// <summary>
        /// Subscribes the user.
        /// </summary>
        /// <param name="customerId">The customer stripe identifier.</param>
        /// <param name="planId">The plan stripe identifier.</param>
        /// <param name="trialInDays">The trial in days.</param>
        /// <param name="taxPercent">The tax percent.</param>
        Task<string> SubscribeUserAsync(string customerId, string planId, int trialInDays = 0, decimal taxPercent = 0);

        /// <summary>
        /// Ends the subscription.
        /// </summary>
        /// <param name="subStripeId">The sub stripe identifier.</param>
        /// <param name="cancelAtPeriodEnd">if set to <c>true</c> [cancel at period end].</param>
        /// <returns>The date when the subscription will be cancelled</returns>
        Task<DateTime> EndSubscriptionAsync(string subStripeId, bool cancelAtPeriodEnd = false);

        /// <summary>
        /// Updates the subscription. (Change subscription plan)
        /// </summary>
        /// <param name="subStripeId">The sub stripe identifier.</param>
        /// <param name="newPlanId">The new plan identifier.</param>
        /// <param name="proRate">if set to <c>true</c> [pro rate].</param>
        /// <returns></returns>
        Task UpdateSubscriptionAsync(string subStripeId, string newPlanId, bool proRate);

        /// <summary>
        /// Subscribes the user natural month.
        /// </summary>
        /// <param name="stripeCustomerId">The stripe customer identifier.</param>
        /// <param name="planId">The plan identifier.</param>
        /// <param name="billingAnchorCycle">The billing anchor cycle.</param>
        /// <param name="taxPercent">The tax percent.</param>
        /// <returns></returns>
        Task<StripeSubscription> SubscribeUserNaturalMonthAsync(string stripeCustomerId, string planId, DateTime? billingAnchorCycle, decimal taxPercent);
    }
}