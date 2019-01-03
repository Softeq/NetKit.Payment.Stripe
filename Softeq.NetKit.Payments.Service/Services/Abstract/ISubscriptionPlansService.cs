// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Softeq.NetKit.Payments.Service.TransportModels.SubscriptionPlan.Request;
using Softeq.NetKit.Payments.Service.TransportModels.SubscriptionPlan.Response;

namespace Softeq.NetKit.Payments.Service.Services.Abstract
{
    public interface ISubscriptionPlansService
    {
        /// <summary>
        /// Gets all subscription plans asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SubscriptionPlanResponse>> GetAllAsync();

        /// <summary>
        /// Adds the subscription plan asynchronous.
        /// </summary>
        /// <param name="request">The subscription plan request model.</param>
        /// <returns></returns>
        Task AddAsync(CreateSubscriptionPlanRequest request);

        /// <summary>
        /// Updates the subscription plan asynchronous.
        /// </summary>
        /// <param name="request">The subscription plan request model.</param>
        /// <returns></returns>
        Task UpdateAsync(UpdateSubscriptionPlanRequest request);

        /// <summary>
        /// Deletes the subscription plan asynchronous.
        /// </summary>
        /// <param name="subscriptionPlanId">The subscription plan identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">subscriptionPlanId</exception>
        Task DeleteAsync(Guid subscriptionPlanId);

        /// <summary>
        /// Finds the subscription plan asynchronous.
        /// </summary>
        /// <param name="subscriptionPlanId">The subscription plan identifier.</param>
        /// <returns>The Subscription Plan</returns>
        Task<SubscriptionPlanResponse> FindAsync(Guid subscriptionPlanId);
    }
}