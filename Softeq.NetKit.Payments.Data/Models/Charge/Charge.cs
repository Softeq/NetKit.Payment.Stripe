// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using Softeq.NetKit.Payments.Data.Abstractions;
using Softeq.NetKit.Payments.Data.Models.Card;

namespace Softeq.NetKit.Payments.Data.Models.Charge
{
    public class Charge : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public string StripeId { get; set; }
        public string Currency { get; set; }
        public int? Amount { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public virtual CreditCard CreditCard { get; set; }
        public Guid CreditCardId { get; set; }
        public string SaasUserId { get; set; }
        public virtual User.User User { get; set; }
        public Guid UserId { get; set; }
    }
}