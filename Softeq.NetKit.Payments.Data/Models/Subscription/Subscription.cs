// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.ComponentModel.DataAnnotations;
using Softeq.NetKit.Payments.Data.Abstractions;

namespace Softeq.NetKit.Payments.Data.Models.Subscription
{
    /// <summary>
    /// Subscription
    /// </summary>
    public class Subscription : IBaseEntity<Guid>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        public DateTime? Start { get; set; }

        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        /// <value>
        /// The end.
        /// </value>
        public DateTime? End { get; set; }

        /// <summary>
        /// Gets or sets the trial start.
        /// </summary>
        /// <value>
        /// The trial start.
        /// </value>
        public DateTime? TrialStart { get; set; }

        /// <summary>
        /// Gets or sets the trial end.
        /// </summary>
        /// <value>
        /// The trial end.
        /// </value>
        public DateTime? TrialEnd { get; set; }

        /// <summary>
        /// Gets or sets the subscription plan identifier.
        /// </summary>
        /// <value>
        /// The subscription plan identifier.
        /// </value>
        public Guid SubscriptionPlanId { get; set; }

        /// <summary>
        /// Gets or sets the subscription plan.
        /// </summary>
        /// <value>
        /// The subscription plan.
        /// </value>
        public virtual SubscriptionPlan.SubscriptionPlan SubscriptionPlan { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public virtual User.User User { get; set; }

        public string SaasUserId { get; set; }

        /// <summary>
        /// Gets or sets the stripe identifier.
        /// </summary>
        /// <value>
        /// The stripe identifier.
        /// </value>
        //[Index]
        [MaxLength(50)]
        public string StripeId { get; set; }

        /// <summary>
        /// Subscription status: Possible values are trialing, active, past_due, canceled, or unpaid. A subscription still in its trial period is trialing and moves to active when the trial period is over. When payment to renew the subscription fails, the subscription becomes past_due. After Stripe has exhausted all payment retry attempts, the subscription ends up with a status of either canceled or unpaid depending on your retry settings. 
        /// </summary>
        /// <returns></returns>
        public string Status { get; set; }

        /// <summary>
        /// Update the amount of tax applied to this subscription. Changing the tax_percent of a subscription will only affect future invoices.
        /// </summary>
        /// <value>
        /// The tax percent.
        /// </value>
        public decimal TaxPercent { get; set; }

        /// <summary>
        /// Gets or sets the reason to cancel.
        /// </summary>
        /// <value>
        /// The reason to cancel.
        /// </value>
        public string CancelReason { get; set; }

        /// <summary>
        /// Determines whether this instance is trialing.
        /// </summary>
        /// <returns></returns>
        public bool IsTrialing()
        {
            return TrialStart != null && TrialEnd != null && TrialEnd > DateTime.UtcNow;
        }
    }
}