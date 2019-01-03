// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using Softeq.NetKit.Payments.Data.Abstractions;

namespace Softeq.NetKit.Payments.Data.Models.Invoice
{
    /// <summary>
    /// Invoice Plan
    /// </summary>
    public class InvoicePlan : IBaseEntity<Guid>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the stripe plan identifier.
        /// </summary>
        /// <value>
        /// The stripe plan identifier.
        /// </value>
        public string StripePlanId { get; set; }

        /// <summary>
        /// Gets or sets the interval.
        /// One of day, week, month or year. The frequency with which a subscription should be billed.
        /// </summary>
        /// <value>
        /// The interval.
        /// </value>
        public string Interval { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        /// <value>
        /// The created.
        /// </value>
        public DateTime? Created { get; set; }

        /// <summary>
        /// Gets or sets the amount in cents.
        /// The amount in cents to be charged on the interval specified
        /// </summary>
        /// <value>
        /// The amount in cents.
        /// </value>
        public int? AmountInCents { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the interval count.
        /// The number of intervals (specified in the interval property) between each subscription billing. For example, interval=month and interval_count=3 bills every 3 months.
        /// </summary>
        /// <value>
        /// The interval count.
        /// </value>
        public int IntervalCount { get; set; }

        /// <summary>
        /// Gets or sets the trial period days.
        /// Number of trial period days granted when subscribing a customer to this plan. Null if the plan has no trial period.
        /// </summary>
        /// <value>
        /// The trial period days.
        /// </value>
        public int? TrialPeriodDays { get; set; }
    }
}