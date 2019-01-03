// Developed by Softeq Development Corporation
// http://www.softeq.com

namespace Softeq.NetKit.Payments.Service.TransportModels.Customer.Request
{
    public class CreateEphemeralKeyRequest
    {
        public CreateEphemeralKeyRequest(string userId, string email, string stripeVersion)
        {
            UserId = userId;
            Email = email;
            StripeVersion = stripeVersion;
        }

        public string UserId { get; set; }
        public string Email { get; set; }
        public string StripeVersion { get; set; }
    }
}