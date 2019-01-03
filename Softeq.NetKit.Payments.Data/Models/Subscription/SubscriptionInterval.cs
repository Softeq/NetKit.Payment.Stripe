// Developed by Softeq Development Corporation
// http://www.softeq.com

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Softeq.NetKit.Payments.Data.Models.Subscription
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SubscriptionInterval
    {
        /// <summary>
        /// Monthly
        /// </summary>
        Monthly = 1,

        /// <summary>
        /// Yearly
        /// </summary>
        Yearly = 2,

        /// <summary>
        /// Weekly
        /// </summary>
        Weekly = 3,

        /// <summary>
        /// Every 6 months
        /// </summary>
        EverySixMonths = 4,

        /// <summary>
        /// Every 3 months
        /// </summary>
        EveryThreeMonths = 5
    }
}