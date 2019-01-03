// Developed by Softeq Development Corporation
// http://www.softeq.com

using Softeq.NetKit.Payments.Service.TransportModels.SubscriptionPlan.Response;

namespace Softeq.NetKit.Payments.Service.TransportModels.Mappers
{
    public static class SubscriptionPlanMapper
    {
        public static SubscriptionPlanResponse ToSubscriptionPlanResponse(this Data.Models.SubscriptionPlan.SubscriptionPlan plan)
        {
            var newPlan = new SubscriptionPlanResponse();
            if (plan != null)
            {
                newPlan.Id = plan.Id;
                newPlan.StripeId = plan.StripeId;
                newPlan.Currency = plan.Currency;
                newPlan.Status = plan.Status;
                newPlan.Interval = plan.Interval;
                newPlan.Name = plan.Name;
                newPlan.Price = plan.Price;
                newPlan.TrialPeriodInDays = plan.TrialPeriodInDays;
            }

            return newPlan;
        }
    }
}