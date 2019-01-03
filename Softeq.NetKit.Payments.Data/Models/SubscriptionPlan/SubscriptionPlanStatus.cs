// Developed by Softeq Development Corporation
// http://www.softeq.com

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Softeq.NetKit.Payments.Data.Models.SubscriptionPlan
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SubscriptionPlanStatus
    {
        Enabled = 0,
        Disabled = 1
    }
}