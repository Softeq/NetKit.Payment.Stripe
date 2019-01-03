// Developed by Softeq Development Corporation
// http://www.softeq.com

namespace Softeq.NetKit.Payments.Service.TransportModels.User.Request
{
    public class UpdateUserRequest
    {
        public UpdateUserRequest(string userId, string stripeCustomerId)
        {
            UserId = userId;
            StripeCustomerId = stripeCustomerId;
        }

        public string UserId { get; set; }
        public string StripeCustomerId { get; set; }
    }
}