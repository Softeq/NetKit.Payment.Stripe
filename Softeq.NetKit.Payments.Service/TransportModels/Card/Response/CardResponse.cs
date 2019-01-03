// Developed by Softeq Development Corporation
// http://www.softeq.com

namespace Softeq.NetKit.Payments.Service.TransportModels.Card.Response
{
    public class CardResponse
    {
        public string StripeId { get; set; }
        public string StripeCustomerId { get; set; }
        public int? ExpirationMonth { get; set; }
        public int? ExpirationYear { get; set; }
        public string Last4 { get; set; }
        public string Fingerprint { get; set; }
        public string SaasUserId { get; set; }
    }
}