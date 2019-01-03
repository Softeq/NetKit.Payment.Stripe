// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Collections.Generic;
using System.Linq;
using Softeq.NetKit.Payments.Data.Models.Subscription;
using Softeq.NetKit.Payments.Data.Models.SubscriptionPlan;
using Softeq.NetKit.Payments.Service.PaymentProcessor.Interfaces;
using Softeq.NetKit.Payments.Service.TransportModels.SubscriptionPlan.Request;
using Stripe;

namespace Softeq.NetKit.Payments.Service.PaymentProcessor.Stripe
{
    /// <summary>
    /// Subscription Plan Provider
    /// </summary>
    public class SubscriptionPlanProvider : ISubscriptionPlanProvider
    {
        private readonly StripePlanService _planService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionPlanProvider"/> class.
        /// </summary>
        /// <param name="settings">Stripe configuration settings.</param>
        public SubscriptionPlanProvider(StripeSettings settings)
        {
            _planService = new StripePlanService(settings.ApiKey);
        }

        /// <inheritdoc />
        public object Add(CreateSubscriptionPlanRequest plan)
        {
            var result = _planService.Create(new StripePlanCreateOptions
            {
                ProductId = plan.ProductId,
                Nickname = plan.Name,
                Amount = (int) Math.Round(plan.Price * 100),
                Currency = plan.Currency,
                Interval = GetInterval(plan.Interval),
                TrialPeriodDays = plan.TrialPeriodInDays,
                IntervalCount = 1, // The number of intervals (specified in the interval property) between each subscription billing. For example, interval=month and interval_count=3 bills every 3 months.
                Metadata = GetSubscriptionPlanProperties(plan.Properties.ToList()),
            });

            return result;
        }

        /// <inheritdoc />
        public object Update(SubscriptionPlan plan)
        {
            var res = _planService.Update(plan.StripeId, new StripePlanUpdateOptions
            {
                Nickname = plan.Name,
                Metadata = GetSubscriptionPlanProperties(plan.Properties.ToList())
            });

            return res;
        }

        /// <inheritdoc />
        public void Delete(string planId)
        {
            _planService.Delete(planId);
        }

        private static string GetInterval(SubscriptionInterval interval)
        {
            string result = null;

            switch (interval)
            {
                case SubscriptionInterval.Monthly:
                    result = "month";
                    break;
                case SubscriptionInterval.Yearly:
                    result = "year";
                    break;
                case SubscriptionInterval.Weekly:
                    result = "week";
                    break;
                case SubscriptionInterval.EveryThreeMonths:
                    result = "3-month";
                    break;
                case SubscriptionInterval.EverySixMonths:
                    result = "6-month";
                    break;
            }

            return result;
        }

        private static Dictionary<string, string> GetSubscriptionPlanProperties(List<SubscriptionPlanProperty> properties)
        {
            var newProperties = new Dictionary<string, string>();
            foreach (var property in properties)
            {
                newProperties.Add(property.Key, property.Value);
            }

            return newProperties;
        }
    }
}