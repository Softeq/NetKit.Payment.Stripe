// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using Softeq.NetKit.Payments.Data.Abstractions;

namespace Softeq.NetKit.Payments.Data.Models.Invoice
{
    /// <summary>
    /// Invoice Line Item
    /// </summary>
    public class InvoiceLineItem : IBaseEntity<Guid>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the stripe line item identifier.
        /// </summary>
        /// <value>
        /// The stripe line item identifier.
        /// </value>
        public string StripeLineItemId { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public int? Amount { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="InvoiceLineItem"/> is proration.
        /// Whether or not the invoice item was created automatically as a proration adjustment when the customer switched plans
        /// </summary>
        /// <value>
        ///   <c>true</c> if proration; otherwise, <c>false</c>.
        /// </value>
        public bool Proration { get; set; }

        /// <summary>
        /// Gets or sets the period.
        /// </summary>
        /// <value>
        /// The period.
        /// </value>
        public InvoicePeriod Period { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// If the invoice item is a proration, the quantity of the subscription that the proration was computed for.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        public int? Quantity { get; set; }

        /// <summary>
        /// Gets or sets the plan.
        /// </summary>
        /// <value>
        /// The plan.
        /// </value>
        public InvoicePlan Plan { get; set; }
    }
}