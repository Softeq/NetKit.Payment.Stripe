// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Collections.Generic;
using Softeq.NetKit.Payments.Data.Models.Subscription;
using Softeq.NetKit.Payments.Data.Models.SubscriptionPlan;

namespace Softeq.NetKit.Payments.Service.TransportModels.SubscriptionPlan.Request
{
    public class CreateSubscriptionPlanRequest
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
        public SubscriptionInterval Interval { get; set; }
        public int TrialPeriodInDays { get; set; }
        public SubscriptionPlanStatus Status { get; set; }
        public List<SubscriptionPlanProperty> Properties { get; set; }
    }
}