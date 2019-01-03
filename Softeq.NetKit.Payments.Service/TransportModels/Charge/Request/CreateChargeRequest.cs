// Developed by Softeq Development Corporation
// http://www.softeq.com

namespace Softeq.NetKit.Payments.Service.TransportModels.Charge.Request
{
    public class CreateChargeRequest
    {
        public string UserId { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public string CardSourceId { get; set; }
    }
}