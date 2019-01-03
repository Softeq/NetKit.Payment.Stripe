// Developed by Softeq Development Corporation
// http://www.softeq.com

using Softeq.NetKit.Payments.Data.Models.SubscriptionPlan;
using Softeq.NetKit.Payments.Service.TransportModels.SubscriptionPlan.Request;

namespace Softeq.NetKit.Payments.Service.PaymentProcessor.Interfaces
{
    /// <summary>
    /// Interface for CRUD related to subscription plans with Stripe
    /// </summary>
    public interface ISubscriptionPlanProvider
    {
        /// <summary>
        /// Adds the specified plan.
        /// </summary>
        /// <param name="request">The plan request model.</param>
        /// <returns></returns>
        object Add(CreateSubscriptionPlanRequest request);

        /// <summary>
        /// Updates the specified plan.
        /// </summary>
        /// <param name="plan">The plan.</param>
        /// <returns></returns>
        object Update(SubscriptionPlan plan);

        /// <summary>
        /// Deletes the specified plan identifier.
        /// </summary>
        /// <param name="planId">The plan identifier.</param>
        void Delete(string planId);
    }
}