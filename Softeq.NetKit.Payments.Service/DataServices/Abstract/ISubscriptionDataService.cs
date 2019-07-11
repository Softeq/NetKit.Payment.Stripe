// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Softeq.NetKit.Payments.Data.Models.Subscription;
using Softeq.NetKit.Payments.Service.Exceptions;

namespace Softeq.NetKit.Payments.Service.DataServices.Abstract
{
    /// <summary>
    /// Interface for CRUD related to subscriptions in the database.
    /// </summary>
    public interface ISubscriptionDataService
    {
        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="subscriptionId">The subscription stripe identifier.</param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        Task<Subscription> FindByIdAsync(string subscriptionId);

        /// <summary>
        /// Subscribes the user asynchronous.
        /// </summary>
        /// <param name="saasUserId">The saas user identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="planId">The plan identifier.</param>
        /// <param name="trialPeriodInDays">The trial period in days.</param>
        /// <param name="taxPercent">The tax percent.</param>
        /// <param name="subscriptionId">The stripe identifier.</param>
        /// <returns>
        /// The subscription
        /// </returns>
        /// <exception cref="NotFoundException"></exception>
        Task<Subscription> SubscribeUserAsync(string saasUserId, Guid userId, string planId, string subscriptionId = null, int? trialPeriodInDays = null, decimal taxPercent = 0);

        /// <summary>
        /// Gets the User's subscriptions asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<List<Subscription>> UserSubscriptionsAsync(string userId);

        /// <summary>
        /// Get the User's active subscription asynchronous. Only the first (valid if your customers can have only 1 subscription at a time).
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="subscriptionId">The subscription identifier.</param>
        /// <returns>The subscription</returns>
        /// <exception cref="NotFoundException"></exception>
        Task<Subscription> UserActiveSubscriptionAsync(string userId, string subscriptionId);

        /// <summary>
        /// Get the User's active subscriptions asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The list of Subscriptions</returns>
        Task<List<Subscription>> UserActiveSubscriptionsAsync(string userId);

        /// <summary>
        /// Ends the subscription asynchronous.
        /// </summary>
        /// <param name="subscriptionId">The subscription stripe identifier.</param>
        /// <param name="subscriptionEnDateTime">The subscription en date time.</param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        Task CancelSubscriptionAsync(string subscriptionId, DateTime subscriptionEnDateTime);

        /// <summary>
        /// Updates the subscription asynchronous.
        /// </summary>
        /// <param name="subscription">The subscription.</param>
        /// <returns></returns>
        Task UpdateSubscriptionAsync(Subscription subscription);

        /// <summary>
        /// Updates the subscription tax.
        /// </summary>
        /// <param name="subscriptionId">The subscription stripe identifier.</param>
        /// <param name="taxPercent">The tax percent.</param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        Task UpdateSubscriptionTax(Guid subscriptionId, decimal taxPercent);

        /// <summary>
        /// Deletes the subscriptions asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task CancelSubscriptionsAsync(string userId);
    }
}