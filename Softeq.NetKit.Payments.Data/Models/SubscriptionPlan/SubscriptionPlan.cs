// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Softeq.NetKit.Payments.Data.Abstractions;
using Softeq.NetKit.Payments.Data.Models.Subscription;

namespace Softeq.NetKit.Payments.Data.Models.SubscriptionPlan
{
    /// <summary>
    /// Subscription Plan
    /// </summary>
    public sealed class SubscriptionPlan : IBaseEntity<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionPlan"/> class.
        /// </summary>
        public SubscriptionPlan()
        {
            Properties = new List<SubscriptionPlanProperty>();
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Display(Name = "Plan Identifier")]
        [Required(ErrorMessage = "Please set a plan identifier.")]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the stripe plan identifier.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string StripeId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        [Required]
        [Range(0.0, 1000000)]
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the interval.
        /// </summary>
        /// <value>
        /// The interval.
        /// </value>
        [Required]
        public SubscriptionInterval Interval { get; set; }

        /// <summary>
        /// Gets or sets the trial period in days.
        /// </summary>
        /// <value>
        /// The trial period in days.
        /// </value>
        [Display(Name = "Trial period in days")]
        [Range(0, 365)]
        public int TrialPeriodInDays { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SubscriptionPlan"/> is disabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if disabled; otherwise, <c>false</c>.
        /// </value>
        public SubscriptionPlanStatus Status { get; set; }

        /// <summary>
        /// Collection of properties related to this plan (Maximum users, storage, etc)
        /// </summary>
        public ICollection<SubscriptionPlanProperty> Properties { get; set; }
    }
}