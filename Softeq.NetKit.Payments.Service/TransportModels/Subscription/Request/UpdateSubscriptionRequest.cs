// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;

namespace Softeq.NetKit.Payments.Service.TransportModels.Subscription.Request
{
    public class UpdateSubscriptionRequest
    {
        public UpdateSubscriptionRequest(string subscriptionId, string userId, Guid planId, bool prorate = true)
        {
            SubscriptionId = subscriptionId;
            UserId = userId;
            PlanId = planId;
            Prorate = prorate;
        }

        public string UserId { get; set; }
        public string SubscriptionId { get; set; }
        public Guid PlanId { get; set; }
        public bool Prorate { get; set; }
    }
}