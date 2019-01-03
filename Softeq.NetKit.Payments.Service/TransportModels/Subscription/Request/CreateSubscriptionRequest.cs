// Developed by Softeq Development Corporation
// http://www.softeq.com

namespace Softeq.NetKit.Payments.Service.TransportModels.Subscription.Request
{
    public class CreateSubscriptionRequest
    {
        public CreateSubscriptionRequest(string userId, string email, string planId, string cardId, decimal taxPercent = 0)
        {
            UserId = userId;
            Email = email;
            PlanId = planId;
            CardId = cardId;
            TaxPercent = taxPercent;
        }

        // User fields
        public string UserId { get; set; }
        public string Email { get; set; }

        // Subscription fields
        public string PlanId { get; set; }
        public decimal TaxPercent { get; set; }

        // Credit card
        public string CardId { get; set; }
    }
}