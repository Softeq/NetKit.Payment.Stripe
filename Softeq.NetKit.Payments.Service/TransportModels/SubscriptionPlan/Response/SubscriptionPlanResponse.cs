// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Collections.Generic;
using Softeq.NetKit.Payments.Data.Models.Subscription;
using Softeq.NetKit.Payments.Data.Models.SubscriptionPlan;

namespace Softeq.NetKit.Payments.Service.TransportModels.SubscriptionPlan.Response
{
    public class SubscriptionPlanResponse
    {
        public Guid Id { get; set; }
        public string StripeId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
        public SubscriptionInterval Interval { get; set; }
        public int TrialPeriodInDays { get; set; }
        public SubscriptionPlanStatus Status { get; set; }
        public List<SubscriptionPlanProperty> Properties { get; set; }
    }
}