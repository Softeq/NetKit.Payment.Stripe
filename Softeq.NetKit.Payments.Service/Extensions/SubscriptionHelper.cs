// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Collections.Generic;
using Softeq.NetKit.Payments.Data.Models.Subscription;

namespace Softeq.NetKit.Payments.Service.Extensions
{
    public static class SubscriptionHelper
    {
        public static List<Subscription> UpdateSubscriptionTimeEnd(this List<Subscription> subscriptions)
        {
            var newSubscriptions = new List<Subscription>();
            foreach (var subscription in subscriptions)
            {
                subscription.End = DateTime.UtcNow;
                newSubscriptions.Add(subscription);
            }

            return newSubscriptions;
        }
    }
}