// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Softeq.NetKit.Payments.Data.Models.Subscription;
using Softeq.NetKit.Payments.Service.TransportModels.Subscription.Request;

namespace Softeq.NetKit.Payments.Service.Services.Abstract
{
    public interface ISubscriptionsService
    {
        /// <summary>
        /// Subscribes the user to a Stripe plan. If the user doesn't exist in Stripe, is created
        /// </summary>
        /// <param name="model">Create subscription request model</param>
        /// <returns>
        /// Subscription
        /// </returns>
        Task<Subscription> SubscribeUserAsync(CreateSubscriptionRequest model);

        /// <summary>
        /// Cancel subscription from Stripe
        /// </summary>
        /// <param name="request">End subscription request model</param>
        /// <returns>The Date when the subscription ends (it can be future if cancelAtPeriodEnd is true)</returns>
        Task<DateTime?> CancelSubscriptionAsync(EndSubscriptionRequest request);

        /// <summary>
        /// Updates the subscription asynchronous, if the new plan is more expensive the customer is charged immediately
        /// </summary>
        /// <param name="request">The update subscription request model.</param>
        /// <returns></returns>
        Task UpdateSubscriptionAsync(UpdateSubscriptionRequest request);

        /// <summary>
        /// Get a list of active subscriptions for the User
        /// </summary>
        /// <param name="userId">Application User PlanId</param>
        /// <returns>List of Active Subscriptions</returns>
        Task<List<Subscription>> GetUserActiveSubscriptionsAsync(string userId);

        /// <summary>This method returns the number of days of trial left for a given user. It will return 0 if there aren't any days left or no active subscriptions for the user.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="subscriptionId">The subscription identifier.</param>
        /// <returns></returns>
        /// <exception caption="" cref="System.NotImplementedException"></exception>
        Task<int> GetDaysTrialLeftAsync(string userId, string subscriptionId);

        /// <summary>
        /// Subscribes the user, with a billing cycle that goes from the 1st of the month asynchronous.
        /// Creates the user in Stripe if doesn't exist already.
        /// Saves de Subscription data in the database if the subscription suceeds.
        /// </summary>
        /// <param name="request">Create subscription request model.</param>
        /// <returns></returns>
        Task SubscribeUserWithBillingCycleAsync(CreateSubscriptionRequest request);

        /// <summary>
        /// Deletes the subscriptions.
        /// </summary>
        /// <param name="request">The end subscriptions request.</param>
        /// <returns></returns>
        Task CancelSubscriptionsAsync(EndSubscriptionsRequest request);
    }
}