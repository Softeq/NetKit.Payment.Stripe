// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Softeq.NetKit.Payments.Data.Models.SubscriptionPlan;
using Softeq.NetKit.Payments.Service.Exceptions;

namespace Softeq.NetKit.Payments.Service.DataServices.Abstract
{
    /// <summary>
    /// Interface for CRUD related to subscription plans in the database.
    /// </summary>
    public interface ISubscriptionPlanDataService
    {
        /// <summary>
        /// Gets all subscription plans asynchronous.
        /// </summary>
        /// <returns>List of Subscription Plans</returns>
        Task<List<SubscriptionPlan>> GetAllAsync();

        /// <summary>
        /// Finds the subscription plans asynchronous.
        /// </summary>
        /// <param name="subscriptionPlanId">The subscription plan identifier.</param>
        /// <returns>The subscription plan</returns>
        /// <exception cref="NotFoundException"></exception>
        Task<SubscriptionPlan> FindAsync(Guid subscriptionPlanId);

        /// <summary>
        /// Adds the subscription plan asynchronous.
        /// </summary>
        /// <param name="subscriptionPlan">The subscription plan.</param>
        /// <returns></returns>
        Task AddAsync(SubscriptionPlan subscriptionPlan);

        /// <summary>
        /// Updates the subscription plan asynchronous.
        /// </summary>
        /// <param name="subscriptionPlan">The subscription plan.</param>
        /// <returns>int</returns>
        Task UpdateAsync(SubscriptionPlan subscriptionPlan);

        /// <summary>
        /// Deletes the subscription plan asynchronous.
        /// </summary>
        /// <param name="subscriptionPlanId">The subscription plan identifier.</param>
        /// <returns>int</returns>
        Task<int> DeleteAsync(Guid subscriptionPlanId);

        /// <summary>
        /// Disables the subscription plan asynchronous. Useful when you don't want to subscribe more users to this plan, but you still want to maintain your current subscribers.
        /// </summary>
        /// <param name="subscriptionPlanId">The subscription plan identifier.</param>
        /// <returns>int</returns>
        Task<int> DisableAsync(Guid subscriptionPlanId);

        /// <summary>
        /// Counts the users subscribed to a given plan asynchronous.
        /// </summary>
        /// <param name="subscriptionPlanId">The subscription plan identifier.</param>
        /// <returns>The number of users subscribed.</returns>
        Task<int> CountUsersAsync(Guid subscriptionPlanId);
    }
}