// Developed by Softeq Development Corporation
// http://www.softeq.com

namespace Softeq.NetKit.Payments.Service.TransportModels.Subscription.Request
{
    public class EndSubscriptionRequest
    {
        public EndSubscriptionRequest(string subscriptionId, string userId, bool cancelAtPeriodEnd)
        {
            SubscriptionId = subscriptionId;
            UserId = userId;
            CancelAtPeriodEnd = cancelAtPeriodEnd;
        }

        public string SubscriptionId { get; set; }
        public string UserId { get; set; }
        public bool CancelAtPeriodEnd { get; set; }
    }
}