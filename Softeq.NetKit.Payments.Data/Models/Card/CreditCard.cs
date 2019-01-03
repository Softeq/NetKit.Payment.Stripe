// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Softeq.NetKit.Payments.Data.Abstractions;

namespace Softeq.NetKit.Payments.Data.Models.Card
{
    /// <summary>
    /// Credit Card
    /// </summary>
    public class CreditCard : IBaseEntity<Guid>
    {
        /// <summary>
        /// Gets or sets the stripe identifier.
        /// </summary>
        /// <value>
        /// The stripe identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the stripe identifier.
        /// </summary>
        /// <value>
        /// The stripe token.
        /// </value>
        public string StripeId { get; set; }

        /// <summary>
        /// Gets or sets the stripe token. Represents a credit card stored in Stripe.
        /// </summary>
        /// <value>
        /// The stripe token.
        /// </value>
        [NotMapped]
        public string StripeToken { get; set; }

        /// <summary>
        /// Gets or sets the name on the card.
        /// </summary>
        /// <value>
        /// The name on the card.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the last 4 digits of the credit card.
        /// </summary>
        /// <value>
        /// The last4.
        /// </value>
        public string Last4 { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Uniquely identifies this particular card number. You can use this attribute to check whether two customers who’ve signed up with you are using the same card number, for example.
        /// </summary>
        /// <value>
        /// The fingerprint.
        /// </value>
        public string Fingerprint { get; set; }

        /// <summary>
        /// Gets or sets the expiration month.
        /// </summary>
        /// <value>
        /// The expiration month.
        /// </value>
        [Required]
        [Range(1, 12)]
        public int? ExpirationMonth { get; set; }

        /// <summary>
        /// Gets or sets the expiration year.
        /// </summary>
        /// <value>
        /// The expiration year.
        /// </value>
        [Required]
        //[Range(2015, 2030)]
        public int? ExpirationYear { get; set; }

        /// <summary>
        /// Gets or sets the Two-letter ISO code representing the country of the card.  (This is returned by Stripe)
        /// </summary>
        /// <value>
        /// The card country.
        /// </value>
        public string CardCountry { get; set; }

        /// <summary>
        /// Gets or sets the saas ecom user identifier.
        /// </summary>
        /// <value>
        /// The saas ecom user identifier.
        /// </value>
        public string SaasUserId { get; set; }

        public string StripeCustomerId { get; set; }

        public virtual User.User User { get; set; }
        public Guid UserId { get; set; }

        public virtual IList<Charge.Charge> Charges { get; set; }
    }
}