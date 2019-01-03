// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;

namespace Softeq.NetKit.Payments.Service.TransportModels.Charge.Response
{
    public class ChargeResponse
    {
        public string StripeId { get; set; }
        public string Currency { get; set; }
        public int? Amount { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public string CreditCardId { get; set; }
        public Guid UserId { get; set; }
    }
}