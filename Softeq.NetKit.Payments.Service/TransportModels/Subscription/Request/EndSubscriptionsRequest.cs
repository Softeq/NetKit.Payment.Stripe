// Developed by Softeq Development Corporation
// http://www.softeq.com

namespace Softeq.NetKit.Payments.Service.TransportModels.Subscription.Request
{
    public class EndSubscriptionsRequest
    {
        public EndSubscriptionsRequest(string userId, string customerId, bool cancelAtPeriodEnd)
        {
            UserId = userId;
            CustomerId = customerId;
            CancelAtPeriodEnd = cancelAtPeriodEnd;
        }

        public string UserId { get; set; }
        public string CustomerId { get; set; }
        public bool CancelAtPeriodEnd { get; set; }
    }
}