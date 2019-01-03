// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Collections.Generic;
using Softeq.NetKit.Payments.Data.Models.SubscriptionPlan;

namespace Softeq.NetKit.Payments.Service.TransportModels.SubscriptionPlan.Request
{
    public class UpdateSubscriptionPlanRequest
    {
        public Guid PlanId { get; set; }
        public string ProductId { get; set; }
        public string Name { get; set; }
        public List<SubscriptionPlanProperty> Properties { get; set; }
    }
}