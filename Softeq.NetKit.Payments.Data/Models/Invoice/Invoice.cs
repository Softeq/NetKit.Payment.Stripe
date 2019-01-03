// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Softeq.NetKit.Payments.Data.Abstractions;

namespace Softeq.NetKit.Payments.Data.Models.Invoice
{
    /// <summary>
    /// Invoices are statements of what a customer owes for a particular billing period, including subscriptions, invoice items, and any automatic proration adjustments if necessary.
    /// Once an invoice is created, payment is automatically attempted. Note that the payment, while automatic, does not happen exactly at the time of invoice creation. If you have configured webhooks, the invoice will wait until one hour after the last webhook is successfully sent (or the last webhook times out after failing).
    /// Any customer credit on the account is applied before determining how much is due for that invoice (the amount that will be actually charged). If the amount due for the invoice is less than 50 cents (the minimum for a charge), we add the amount to the customer's running account balance to be added to the next invoice. If this amount is negative, it will act as a credit to offset the next invoice. Note that the customer account balance does not include unpaid invoices; it only includes balances that need to be taken into account when calculating the amount due for the next invoice.
    /// </summary>
    public class Invoice : IBaseEntity<Guid>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the stripe identifier.
        /// </summary>
        /// <value>
        /// The stripe identifier.
        /// </value>
        [MaxLength(50)]
        public string StripeId { get; set; }

        /// <summary>
        /// Gets or sets the stripe customer identifier.
        /// </summary>
        /// <value>
        /// The stripe customer identifier.
        /// </value>
        [MaxLength(50)]
        public string StripeCustomerId { get; set; }

        /// <summary>
        /// Gets or sets the customer.
        /// </summary>
        /// <value>
        /// The customer.
        /// </value>
        public virtual User.User Customer { get; set; }

        public string SaasUserId { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Gets or sets the period start.
        /// </summary>
        /// <value>
        /// The period start.
        /// </value>
        public DateTime? PeriodStart { get; set; }

        /// <summary>
        /// Gets or sets the period end.
        /// </summary>
        /// <value>
        /// The period end.
        /// </value>
        public DateTime? PeriodEnd { get; set; }

        /// <summary>
        /// Gets or sets the line items.
        /// </summary>
        /// <value>
        /// The line items.
        /// </value>
        public virtual ICollection<InvoiceLineItem> LineItems { get; set; }

        /// <summary>
        /// Gets or sets the subtotal.
        /// Total of all subscriptions, invoice items, and prorations on the invoice before any discount is applied
        /// </summary>
        /// <value>
        /// The subtotal.
        /// </value>
        public int? Subtotal { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// Total after discount
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        public int? Total { get; set; }

        /// <summary>
        /// Gets or sets the attempted.
        /// Whether or not an attempt has been made to pay the invoice. An invoice is not attempted until 1 hour after the invoice.created webhook, for example, so you might not want to display that invoice as unpaid to your users.
        /// </summary>
        /// <value>
        /// The attempted.
        /// </value>
        public bool? Attempted { get; set; }

        /// <summary>
        /// Gets or sets the closed.
        /// </summary>
        /// <value>
        /// The closed.
        /// </value>
        public bool? Closed { get; set; }

        /// <summary>
        /// Gets or sets the paid.
        /// Whether or not payment was successfully collected for this invoice. An invoice can be paid (most commonly) with a charge or with credit from the customer’s account balance.
        /// </summary>
        /// <value>
        /// The paid.
        /// </value>
        //[Index]
        public bool? Paid { get; set; }

        /// <summary>
        /// Gets or sets the attempt count.
        /// Number of payment attempts made for this invoice, from the perspective of the payment retry schedule. Any payment attempt counts as the first attempt, and subsequently only automatic retries increment the attempt count. In other words, manual payment attempts after the first attempt do not affect the retry schedule.
        /// </summary>
        /// <value>
        /// The attempt count.
        /// </value>
        public int? AttemptCount { get; set; }

        /// <summary>
        /// Gets or sets the amount due.
        /// Final amount due at this time for this invoice. If the invoice’s total is smaller than the minimum charge amount, for example, or if there is account credit that can be applied to the invoice, the amount_due may be 0. If there is a positive starting_balance for the invoice (the customer owes money), the amount_due will also take that into account. The charge that gets generated for the invoice will be for the amount specified in amount_due.
        /// </summary>
        /// <value>
        /// The amount due.
        /// </value>
        public int? AmountDue { get; set; }

        /// <summary>
        /// Gets or sets the starting balance.
        /// </summary>
        /// <value>
        /// The starting balance.
        /// </value>
        public int? StartingBalance { get; set; }

        /// <summary>
        /// Gets or sets the ending balance.
        /// </summary>
        /// <value>
        /// The ending balance.
        /// </value>
        public int? EndingBalance { get; set; }

        /// <summary>
        /// Gets or sets the next payment attempt.
        /// The time at which payment will next be attempted.
        /// </summary>
        /// <value>
        /// The next payment attempt.
        /// </value>
        public DateTime? NextPaymentAttempt { get; set; }

        /// <summary>
        /// Gets or sets the application fee.
        /// The fee in cents that will be applied to the invoice and transferred to the application owner’s Stripe account when the invoice is paid.
        /// </summary>
        /// <value>
        /// The application fee.
        /// </value>
        public int? ApplicationFee { get; set; }

        /// <summary>
        /// Gets or sets the tax.
        /// The amount of tax included in the total, calculated from tax_percent and the subtotal. If no tax_percent is defined, this value will be null.
        /// </summary>
        /// <value>
        /// The tax.
        /// </value>
        public int? Tax { get; set; }

        /// <summary>
        /// This percentage of the subtotal has been added to the total amount of the invoice, including invoice line items and discounts. This field is inherited from the subscription’s tax_percent field, but can be changed before the invoice is paid. This field defaults to null.
        /// </summary>
        /// <value>
        /// The tax percent.
        /// </value>
        public decimal? TaxPercent { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the billing address.
        /// </summary>
        /// <value>
        /// The billing address.
        /// </value>
        public virtual BillingAddress.BillingAddress BillingAddress { get; set; }

        /// <summary>
        /// Gets the invoice period.
        /// </summary>
        /// <value>
        /// The invoice period.
        /// </value>
        [NotMapped]
        public string InvoicePeriod
        {
            get
            {
                var start = PeriodStart;
                var end = PeriodEnd;

                if (LineItems != null && LineItems.Any())
                {
                    var p = LineItems.First();
                    start = p.Period.Start;
                    end = p.Period.End;
                }

                var startString = start.HasValue ? start.Value.ToString("d MMM yyyy") : string.Empty;
                var endString = end.HasValue ? end.Value.ToString("d MMM yyyy") : string.Empty;
                return $"{startString} - {endString}";
            }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the receipt number.
        /// </summary>
        /// <value>
        /// The receipt number.
        /// </value>
        public string ReceiptNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Plan"/> is forgiven.
        /// Whether or not the invoice has been forgiven. Forgiving an invoice instructs us to update the subscription status as if the invoice were succcessfully paid. Once an invoice has been forgiven, it cannot be unforgiven or reopened.
        /// </summary>
        /// <value>
        ///   <c>true</c> if forgiven; otherwise, <c>false</c>.
        /// </value>
        public bool? Forgiven { get; set; }
    }
}