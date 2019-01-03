// Developed by Softeq Development Corporation
// http://www.softeq.com

namespace Softeq.NetKit.Payments.Service.TransportModels.User.Request
{
    public class CreateUserRequest
    {
        public CreateUserRequest(string userId, string stripeCustomerId, bool delinquent)
        {
            UserId = userId;
            StripeCustomerId = stripeCustomerId;
            Delinquent = delinquent;
        }

        public string UserId { get; set; }
        public string StripeCustomerId { get; set; }
        public bool Delinquent { get; set; }
    }
}