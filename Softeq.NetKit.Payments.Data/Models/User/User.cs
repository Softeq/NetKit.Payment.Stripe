// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Collections.Generic;
using Softeq.NetKit.Payments.Data.Abstractions;
using Softeq.NetKit.Payments.Data.Models.Card;

namespace Softeq.NetKit.Payments.Data.Models.User
{
    public class User : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the stripe customer identifier.
        /// </summary>
        /// <value>
        /// The stripe customer identifier.
        /// </value>
        public string StripeCustomerId { get; set; }

        /// <summary>
        /// Gets or sets the saas user identifier.
        /// </summary>
        /// <value>
        /// The saas user identifier.
        /// </value>
        public string SaasUserId { get; set; }

        /// <summary>
        /// Gets or sets the subscriptions.
        /// </summary>
        /// <value>
        /// The subscriptions.
        /// </value>
        public virtual ICollection<Subscription.Subscription> Subscriptions { get; set; }

        /// <summary>
        /// Gets or sets the invoices.
        /// </summary>
        /// <value>
        /// The invoices.
        /// </value>
        public virtual IList<Invoice.Invoice> Invoices { get; set; }

        /// <summary>
        /// Gets or sets the credit cards.
        /// </summary>
        /// <value>
        /// The credit cards.
        /// </value>
        public virtual IList<CreditCard> CreditCards { get; set; } // The actual credit card number is not stored! 

        public virtual IList<Charge.Charge> Charges { get; set; }

        /// <summary>
        /// Gets or sets the ip address.
        /// </summary>
        /// <value>
        /// The ip address.
        /// </value>
        public string IPAddress { get; set; }

        /// <summary>
        /// Gets or sets the ip address country.
        /// </summary>
        /// <value>
        /// The ip address country.
        /// </value>
        public string IPAddressCountry { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SaasEcomUser"/> is delinquent. Whether or not the latest charge for the customer’s latest invoice has failed
        /// </summary>
        /// <value>
        ///   <c>true</c> if delinquent; otherwise, <c>false</c>.
        /// </value>
        public bool Delinquent { get; set; }

        /// <summary>
        /// Gets or sets the lifetime value for the customer (total spent in the app)
        /// </summary>
        /// <value>
        /// The lifetime value.
        /// </value>
        public decimal LifetimeValue { get; set; }

        public virtual BillingAddress.BillingAddress BillingAddress { get; set; }
    }
}