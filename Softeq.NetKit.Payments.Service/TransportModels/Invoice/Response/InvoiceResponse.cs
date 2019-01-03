// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;

namespace Softeq.NetKit.Payments.Service.TransportModels.Invoice.Response
{
    public class InvoiceResponse
    {
        public Guid Id { get; set; }
        public string StripeId { get; set; }
        public string StripeCustomerId { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? PeriodStart { get; set; }
        public DateTime? PeriodEnd { get; set; }
        public int? Subtotal { get; set; }
        public int? Total { get; set; }
        public bool? Attempted { get; set; }
        public bool? Closed { get; set; }
        public bool? Paid { get; set; }
        public int? AttemptCount { get; set; }
        public int? AmountDue { get; set; }
        public int? StartingBalance { get; set; }
        public int? EndingBalance { get; set; }
        public int? Tax { get; set; }
        public decimal? TaxPercent { get; set; }
        public string Currency { get; set; }
        public bool? Forgiven { get; set; }
    }
}